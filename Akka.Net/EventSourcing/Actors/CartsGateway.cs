using System;
using System.Collections.Generic;
using Akka.Actor;
using EventSourcing.Messages;
using EventSourcing.Messages.Commands;
using EventSourcing.Messages.Events;

namespace EventSourcing.Actors
{
    public class CartsGateway : ReceiveActor
    {
        private readonly Dictionary<Guid, IActorRef> pendingOperations;

        public CartsGateway()
        {
            pendingOperations = new Dictionary<Guid, IActorRef>();

            Receive<InitializeCartCommand>(command => Handle(command));
            Receive<AddItemCommand>(command => Handle(command));
            Receive<RemoveItemCommand>(command => Handle(command));

            Receive<CartInitializedEvent>(message => Handle(message));
            Receive<ItemAddedEvent>(message => Handle(message));
            Receive<ItemRemovedEvent>(message => Handle(message));
            Receive<CommandFailed>(message => Handle(message));
        }

        private void Handle(InitializeCartCommand command)
        {
            var actor = GetCart(command.CartId);
            actor.Tell(command);
            pendingOperations.Add(command.Id, Sender);
        }

        private void Handle(AddItemCommand command)
        {
            var actor = GetCart(command.CartId);
            actor.Tell(command);
            pendingOperations.Add(command.Id, Sender);
        }

        private void Handle(RemoveItemCommand command)
        {
            var actor = GetCart(command.CartId);
            actor.Tell(command);
            pendingOperations.Add(command.Id, Sender);
        }

        private void Handle(CartInitializedEvent message)
        {
            ReplyToSender(message.CommandId, () => new CommandHandled(message.CommandId));
        }

        private void Handle(ItemAddedEvent message)
        {
            ReplyToSender(message.CommandId, () => new CommandHandled(message.CommandId));
        }

        private void Handle(ItemRemovedEvent message)
        {
            ReplyToSender(message.CommandId, () => new CommandHandled(message.CommandId));
        }

        private void Handle(CommandFailed message)
        {
            ReplyToSender(message.CommandId, () => message);
        }

        private IActorRef GetCart(string cartId)
        {
            var actor = Context.Child(cartId);
            if (actor is Nobody)
                actor = Context.ActorOf<Cart>(cartId);

            return actor;
        }

        private void ReplyToSender<T>(Guid commandId, Func<T> responseBuilder)
        {
            var sender = pendingOperations[commandId];
            var message = responseBuilder();
            sender.Tell(message);

            pendingOperations.Remove(commandId);
        }
    }
}