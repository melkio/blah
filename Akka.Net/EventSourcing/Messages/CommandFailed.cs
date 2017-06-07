using System;

namespace EventSourcing.Messages
{
    public class CommandFailed
    {
        public Guid CommandId { get; }
        public string Reason { get; }

        public CommandFailed(Guid commandId, string reason)
        {
            CommandId = commandId;
            Reason = reason;
        }
    }
}