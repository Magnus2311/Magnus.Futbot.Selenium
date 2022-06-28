using Confluent.Kafka;
using System.Net;

namespace Magnus.Futbot.Selenium.Procucers
{
    public abstract class BaseProducer<TKey, TValue>
    {
        public BaseProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "host1:9092,host2:9092",
                ClientId = Dns.GetHostName(),
            };

            Producer = new ProducerBuilder<TKey, TValue>(config).Build();
        }

        public IProducer<TKey, TValue> Producer { get; }

        public abstract string Topic { get; }

        public void Produce(TValue value)
            => Producer.ProduceAsync(Topic, new Message<TKey, TValue> { Value = value });
        
    }
}