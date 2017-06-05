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

        private ActorEnvironment()
        {
            system = ActorSystem.Create("HttpCache");

            ItemsGateway = system.ActorOf<ItemsGatewayActor>();
        }

        public static ActorEnvironment Current => environment.Value;
    }
}