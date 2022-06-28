using Confluent.Kafka;
using Magnus.Futbot.Common.Models.Results;
using Magnus.Futbot.Common.Models.Selenium.Profiles;
using Magnus.Futbot.Selenium.Procucers;
using Magnus.Futbot.Services;

namespace Magnus.Futbot.Selenium.Consumers
{
    public class SecurityCodeConsumer : BaseConsumer<Ignore, SubmitCodeDTO>
    {
        private readonly SubmitCodeProducer _submitCodeProducer;

        public SecurityCodeConsumer(SubmitCodeProducer submitCodeProducer)
        {
            _submitCodeProducer = submitCodeProducer;
        }

        public override string Topic => "Magnus.Futbot.Selenium.Submit.Code";

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Task.Run(() =>
                    {
                        var profileDTO = Consumer.Consume(stoppingToken);
                        var response = LoginSeleniumService.SubmitCode(profileDTO.Message.Value);
                        _submitCodeProducer.Produce(new SubmitCodeResult(profileDTO.Message.Value, response));
                    }, stoppingToken);
                }

                Consumer.Dispose();
                Consumer.Close();
            }, stoppingToken);
    }
}
