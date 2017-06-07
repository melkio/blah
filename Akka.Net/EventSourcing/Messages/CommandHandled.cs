using System;

namespace EventSourcing.Messages
{
    public class CommandHandled
    {
        public Guid CommandId { get; }

        public CommandHandled(Guid commandId)
        {
            CommandId = commandId;
        }
    }
}