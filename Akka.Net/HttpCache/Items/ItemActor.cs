using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Akka.Actor;
using HttpCache.Items.Messages;

namespace HttpCache.Items
{
    public class ItemActor : ReceiveActor
    {
        private readonly IActorRef store;

        private string id;

        public ItemActor(IActorRef store)
        {
            this.store = store;

            Receive<CreateItemRequest>(message => HandleCreateItem(message));
        }

        private void HandleCreateItem(CreateItemRequest request)
        {
            var eTag = ComputeETag(request.Code, request.Description, request.Value);

            store.Tell(new StoreItem(id, request.Code, request.Description, request.Value, eTag));
            Sender.Tell(new CreateItemResponse(id, eTag));
        }

        public override void AroundPreStart()
        {
            id = Self.Path.Name;
        }

        private static string ComputeETag(int code, string description, double value)
        {
            var descriptor = $"{code}/{description}/{value}";

            using (var md5 = MD5.Create())
            {
                var buffer = md5.ComputeHash(Encoding.ASCII.GetBytes(descriptor));

                return buffer
                    .AsEnumerable()
                    .Select(x => x.ToString("X2"))
                    .Aggregate((acc, item) => $"{acc}{item}");
            }
        }

    }
}