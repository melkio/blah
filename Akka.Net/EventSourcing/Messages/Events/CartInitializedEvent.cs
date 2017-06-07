using System;

namespace EventSourcing.Messages.Events
{
    public class CartInitializedEvent
    {
        public Guid CommandId { get; }
        public string CartId { get; }
        public string UserId { get; }

        public CartInitializedEvent(Guid commandId, string cartId, string userId)
        {
            CommandId = commandId;
            CartId = cartId;
            UserId = userId;
        }
    }
}