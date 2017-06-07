using System;
using Akka.Actor;
using EventSourcing.Actors;
using EventSourcing.Messages.Commands;
using EventSourcing.Messages.Events;

namespace EventSourcing
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("EventSourcing");

            var store = system.ActorOf<CartStore>("store");
            var gateway = system.ActorOf(Props.Create(() => new CartsGateway(store)), "carts");
            //var logger = system.ActorOf<ConsoleLogger>("logger");

            //system.EventStream.Subscribe(logger, typeof(CartInitializedEvent));
            //system.EventStream.Subscribe(logger, typeof(ItemAddedEvent));
            //system.EventStream.Subscribe(logger, typeof(ItemRemovedEvent));

            var inbox = Inbox.Create(system);
            inbox.Send(gateway, new InitializeCartCommand(Guid.NewGuid(), "Cart1", "melkio"));
            inbox.Send(gateway, new AddItemCommand(Guid.NewGuid(), "Cart1", "item1", 5));
            inbox.Send(gateway, new AddItemCommand(Guid.NewGuid(), "Cart1", "item2", 1));
            inbox.Send(gateway, new RemoveItemCommand(Guid.NewGuid(), "Cart1", "item1", 3));
            inbox.Send(gateway, new RemoveItemCommand(Guid.NewGuid(), "Cart1", "item2", 2));

            var stop = false;
            do
            {
                try
                {
                    var response = inbox.Receive(TimeSpan.FromSeconds(1));
                    Console.WriteLine(response.GetType());
                }
                catch (Exception)
                {
                    stop = true;
                }
            } while (!stop);

            Console.ReadLine();
        }
    }
}
