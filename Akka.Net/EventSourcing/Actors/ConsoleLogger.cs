using System;
using Akka.Actor;
using EventSourcing.Messages;
using EventSourcing.Messages.Events;

namespace EventSourcing.Actors
{
    public class ConsoleLogger : ReceiveActor
    {
        public ConsoleLogger()
        {
            Receive<CartInitializedEvent>(message => Handle(message));
            Receive<ItemAddedEvent>(message => Handle(message));
            Receive<ItemRemovedEvent>(message => Handle(message));
        }

        private void Handle(CartInitializedEvent message)
        {
            Console.WriteLine($"CartInitializedEvent received for {message.CartId}");
        }

        private void Handle(ItemAddedEvent message)
        {
            Console.WriteLine($"ItemAddedEvent received for {message.CartId}");
            Console.WriteLine($"Item: {message.ItemId}");
            Console.WriteLine($"Quantity: {message.Quantity}");
        }

        private void Handle(ItemRemovedEvent message)
        {
            Console.WriteLine($"ItemRemovedEvent received for {message.CartId}");
            Console.WriteLine($"Item: {message.ItemId}");
            Console.WriteLine($"Quantity: {message.Quantity}");
        }
    }
}