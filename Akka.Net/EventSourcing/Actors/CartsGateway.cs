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
        private readonly IActorRef store;
        private readonly Dictionary<Guid, IActorRef> pendingOperations;

        public CartsGateway(IActorRef store)
        {
            this.store = store;
            pendingOperations = new Dictionary<Guid, IActorRef>();

            Receive<InitializeCartCommand>(command => Handle(command));
            Receive<AddItemCommand>(command => Handle(command));
            Receive<RemoveItemCommand>(command => Handle(command));

            Receive<CartInitializedEvent>(message => this.store.Tell(new StoreEvents(message.CommandId, new object[] { message })));
            Receive<ItemAddedEvent>(message => this.store.Tell(new StoreEvents(message.CommandId, new object[] { message })));
            Receive<ItemRemovedEvent>(message => this.store.Tell(new StoreEvents(message.CommandId, new object[] { message })));
            Receive<CommandFailed>(message => Handle(message));

            Receive<EventsStored>(message => Handle(message));
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

        private void Handle(ItemAddedEvent message)
        {
            store.Tell(new StoreEvents(message.CommandId, new object[] { message }));
        }

        private void Handle(ItemRemovedEvent message)
        {
            store.Tell(new StoreEvents(message.CommandId, new object[] { message }));
        }

        private void Handle(CommandFailed message)
        {
            ReplyToSender(message.CommandId, () => message);
        }

        private void Handle(EventsStored message)
        {
            ReplyToSender(message.CommandId, () => new CommandHandled(message.CommandId));
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