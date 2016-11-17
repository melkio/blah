using MassTransit;

namespace Demo04.Common.Messages
{
    public class ChatMessagePostedMessage : Message, CorrelatedBy<string>
    {
        public string CorrelationId { get; set; }

        public string Account { get; set; }
        public string Room { get; set; }
        public string Text { get; set; }
    }
}
