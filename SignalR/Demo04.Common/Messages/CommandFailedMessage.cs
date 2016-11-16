using MassTransit;

namespace Demo04.Common.Messages
{
    public class CommandFailedMessage : CorrelatedBy<string>
    {
        public string CorrelationId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
