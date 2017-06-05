using Akka.Actor;
using HttpCache.Items.Messages;

namespace HttpCache.Items
{
    public class ItemsGatewayActor : ReceiveActor
    {
        private int counter = 0;

        public ItemsGatewayActor()
        {
            Receive<CreateItemRequest>(message => HandleCreateItem(message));
        }

        private void HandleCreateItem(CreateItemRequest message)
        {
            counter++;

            var props = Props.Create(() => new ItemActor(ActorEnvironment.Current.ItemsStore));
            var actor = Context.ActorOf(props, $"{counter}");
            actor.Forward(message);
        }
    }
}