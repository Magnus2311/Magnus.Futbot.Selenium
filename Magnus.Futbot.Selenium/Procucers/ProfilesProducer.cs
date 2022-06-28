using Confluent.Kafka;
using Magnus.Futbot.Common.Models;

namespace Magnus.Futbot.Selenium.Procucers
{
    public class ProfilesProducer : BaseProducer<Null, ProfileDTO>
    {
        public override string Topic => "Magnus.Futbot.Profiles";
    }
}