using Confluent.Kafka;
using Magnus.Futbot.Common.Models.Results;

namespace Magnus.Futbot.Selenium.Procucers
{
    public class SubmitCodeProducer : BaseProducer<Null, SubmitCodeResult>
    {
        public override string Topic => "Magnus.Futbot.Selenium.Submit.Code.Result";
    }
}
