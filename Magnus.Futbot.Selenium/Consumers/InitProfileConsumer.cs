using Confluent.Kafka;
using Magnus.Futbot.Common.Models.Selenium.Profiles;
using Magnus.Futbot.Selenium.Procucers;
using Magnus.Futbot.Services;

namespace Magnus.Futbot.Selenium.Consumers
{
    public class InitProfileConsumer : BaseConsumer<Ignore, AddProfileDTO>
    {
        private readonly ProfilesProducer _profilesProducer;
        private readonly ILogger<InitProfileConsumer> _logger;

        public InitProfileConsumer(ILogger<InitProfileConsumer> logger,
            ProfilesProducer profilesProducer)
        {
            _logger = logger;
            _profilesProducer = profilesProducer;
        }

        public override string Topic => "Magnus.Futbot.Selenium.Init.Profile";

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Task.Run(() =>
                    {
                        var profileDTO = Consumer.Consume(stoppingToken);
                        var response = InitProfileService.InitProfile(profileDTO.Message.Value);
                        _profilesProducer.Produce(response);
                    }, stoppingToken);
                }

                Consumer.Dispose();
                Consumer.Close();
            }, stoppingToken);
    }
}