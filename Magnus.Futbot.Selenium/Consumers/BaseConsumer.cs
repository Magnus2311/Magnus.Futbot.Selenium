using Confluent.Kafka;

namespace Magnus.Futbot.Selenium.Consumers
{
    public abstract class BaseConsumer<TKey, TValue> : BackgroundService
    {
        public BaseConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "host1:9092,host2:9092",
                GroupId = GetType().Name,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            Consumer = new ConsumerBuilder<TKey, TValue>(config).Build();
            Consumer.Subscribe(Topic);
        }


        public IConsumer<TKey, TValue> Consumer { get; }
        public bool Cancelled { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public abstract string Topic { get; }
    }
}
