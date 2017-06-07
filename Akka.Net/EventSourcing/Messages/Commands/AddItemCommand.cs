using System;

namespace EventSourcing.Messages.Commands
{
    public class AddItemCommand
    {
        public Guid Id { get; }
        public string CartId { get; }
        public string ItemId { get; }
        public int Quantity { get; }

        public AddItemCommand(Guid id, string cartId, string itemId, int quantity)
        {
            Id = id;
            CartId = cartId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}