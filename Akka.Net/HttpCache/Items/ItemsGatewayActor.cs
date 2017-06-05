using Akka.Actor;

namespace HttpCache.Items
{
    public partial class ItemsGatewayActor : ReceiveActor
    {
        public ItemsGatewayActor()
        {
            Receive<CreateItemRequest>(message => HandleCreateItem(message));
        }

        private void HandleCreateItem(CreateItemRequest message)
        {
            Sender.Tell(new CreateItemResponse("ABCDEF"));
        }
    }
}