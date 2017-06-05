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

            var actor = Context.ActorOf<ItemActor>($"{counter}");
            actor.Forward(message);
        }
    }
}