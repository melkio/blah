using Demo04.Common.Commands;
using MassTransit;

namespace Demo04.Common.Messages
{
    public class CommandHandledMessage : CorrelatedBy<string>
    {
        public string CorrelationId { get; set; }
        public string CommandType { get; set; }
    }

    public class CommandHandledMessage<TCommand> : CommandHandledMessage
        where TCommand : Command
    {
        public CommandHandledMessage()
        {
            CommandType = typeof(TCommand).FullName;
        }
    }
}
