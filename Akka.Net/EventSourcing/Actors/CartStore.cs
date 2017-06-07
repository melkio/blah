using System;
using Akka.Actor;
using EventSourcing.Messages;

namespace EventSourcing.Actors
{
    public class CartStore : ReceiveActor
    {
        public CartStore()
        {
            Receive<StoreEvents>(message => Handle(message));
        }

        private void Handle(StoreEvents message)
        {
            // STORE EVENTS SOMEWHERE
            Sender.Tell(new EventsStored(message.CommandId));

            Array.ForEach(message.Events, x => Context.System.EventStream.Publish(x));
        }
    }
}