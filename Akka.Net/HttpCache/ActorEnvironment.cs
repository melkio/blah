using System;
using Akka.Actor;
using HttpCache.Items;

namespace HttpCache
{
    public class ActorEnvironment
    {
        private static readonly Lazy<ActorEnvironment> environment = new Lazy<ActorEnvironment>(() => new ActorEnvironment());

        private readonly ActorSystem system;

        public IActorRef ItemsGateway { get; }
        public IActorRef ItemsStore { get; }

        private ActorEnvironment()
        {
            system = ActorSystem.Create("HttpCache");

            ItemsGateway = system.ActorOf<ItemsGatewayActor>();
            ItemsStore = system.ActorOf<InMemoryItemsStoreActor>();
        }

        public static ActorEnvironment Current => environment.Value;
    }
}