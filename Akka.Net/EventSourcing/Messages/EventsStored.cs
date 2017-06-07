using System;

namespace EventSourcing.Messages
{
    public class EventsStored
    {
        public Guid CommandId { get; }

        public EventsStored(Guid commandId)
        {
            CommandId = commandId;
        }
    }
}