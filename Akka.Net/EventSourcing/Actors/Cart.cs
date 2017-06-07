using System;
using System.Collections.Generic;
using Akka.Actor;
using EventSourcing.Messages;
using EventSourcing.Messages.Commands;
using EventSourcing.Messages.Events;

namespace EventSourcing.Actors
{
    public class Cart : ReceiveActor
    {
        private bool isInitialized;
        private readonly Dictionary<string, int> items;

        public Cart()
        {
            items = new Dictionary<string, int>();

            Receive<InitializeCartCommand>(command => Handle(command));
            Receive<AddItemCommand>(command => Handle(command));
            Receive<RemoveItemCommand>(command => Handle(command));
        }

        private void Handle(InitializeCartCommand command)
        {
            if (isInitialized)
            {
                Sender.Tell(new CommandFailed(command.Id, "Cart already initialized!"));
                return;
            }

            isInitialized = true;
            Sender.Tell(new CartInitializedEvent(command.Id, command.CartId, command.UserId));
        }

        private void Handle(AddItemCommand command)
        {
            if (!isInitialized)
            {
                Sender.Tell(new CommandFailed(command.Id, "Cart is not initialized!"));
                return;
            }

            if (!items.ContainsKey(command.ItemId))
                items.Add(command.ItemId, 0);
            items[command.ItemId] += command.Quantity;
            Sender.Tell(new ItemAddedEvent(command.Id, command.CartId, command.ItemId, command.Quantity));
        }

        private void Handle(RemoveItemCommand command)
        {
            if (!isInitialized)
            {
                Sender.Tell(new CommandFailed(command.Id, "Cart is not initialized!"));
                return;
            }
            if (!items.ContainsKey(command.ItemId) || items[command.ItemId] < command.Quantity)
            {
                Sender.Tell(new CommandFailed(command.Id, "Not enough quantity!"));
                return;
            }

            items[command.ItemId] -= command.Quantity;
            Sender.Tell(new ItemRemovedEvent(command.Id, command.CartId, command.ItemId, command.Quantity));
        }
    }
}