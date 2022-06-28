using Confluent.Kafka;
using Magnus.Futbot.Common.Models.Selenium.Profiles;
using Magnus.Futbot.Services;

namespace Magnus.Futbot.Selenium.Consumers
{
    public class SecurityCodeConsumer : BaseConsumer<Ignore, SubmitCodeDTO>
    {
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
                    }, stoppingToken);
                }

                Consumer.Dispose();
                Consumer.Close();
            }, stoppingToken);
    }
}
