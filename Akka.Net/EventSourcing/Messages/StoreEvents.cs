using System;

namespace EventSourcing.Messages
{
    public class StoreEvents
    {
        public Guid CommandId { get; }
        public object[] Events { get; }

        public StoreEvents(Guid commandId, object[] events)
        {
            CommandId = commandId;
            Events = events;
        }
    }
}