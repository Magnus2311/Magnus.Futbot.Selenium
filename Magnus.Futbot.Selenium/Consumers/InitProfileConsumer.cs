using Confluent.Kafka;
using Magnus.Futbot.Common.Models.Selenium.Profiles;
using Magnus.Futbot.Services;

namespace Magnus.Futbot.Selenium.Consumers
{
    public class InitProfileConsumer : BaseConsumer<Ignore, AddProfileDTO>
    {
        private readonly ILogger<InitProfileConsumer> _logger;

        public InitProfileConsumer(ILogger<InitProfileConsumer> logger)
        {
            _logger = logger;
        }

        public override string Topic => "Init.Profile";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Task.Run(() =>
                {
                    var profileDTO = Consumer.Consume(stoppingToken);
                    InitProfileService.InitProfile(profileDTO.Message.Value);
                }, stoppingToken);
            }
        }
    }
}