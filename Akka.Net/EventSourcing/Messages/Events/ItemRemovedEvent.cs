using System;

namespace EventSourcing.Messages.Events
{
    public class ItemRemovedEvent
    {
        public Guid CommandId { get; }
        public string CartId { get; }
        public string ItemId { get; }
        public int Quantity { get; }

        public ItemRemovedEvent(Guid commandId, string cartId, string itemId, int quantity)
        {
            CommandId = commandId;
            CartId = cartId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}