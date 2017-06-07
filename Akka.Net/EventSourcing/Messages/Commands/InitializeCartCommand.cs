using System;

namespace EventSourcing.Messages.Commands
{
    public class InitializeCartCommand
    {
        public Guid Id { get; }
        public string CartId { get; }
        public string UserId { get; }

        public InitializeCartCommand(Guid id, string cartId, string userId)
        {
            Id = id;
            CartId = cartId;
            UserId = userId;
        }
    }
}