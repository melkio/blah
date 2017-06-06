using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Akka.Actor;
using HttpCache.Items.Messages;

namespace HttpCache.Items
{
    public class ItemsGatewayActor : ReceiveActor
    {
        private readonly IActorRef store;
        private readonly Dictionary<int, string> cache;

        public ItemsGatewayActor(IActorRef store)
        {
            this.store = store;
            cache = new Dictionary<int, string>();

            Receive<CreateItemRequest>(message => HandleCreateItem(message));
        }

        private void HandleCreateItem(CreateItemRequest request)
        {
            var id = cache
                         .Select(x => x.Key)
                         .LastOrDefault() + 1;
            var eTag = ComputeETag(request.Code, request.Description, request.Value);
            cache.Add(id, eTag);

            store.Tell(new StoreItem(id, request.Code, request.Description, request.Value, eTag));
            Sender.Tell(new CreateItemResponse(id, eTag));
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