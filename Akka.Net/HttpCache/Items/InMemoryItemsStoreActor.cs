using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using HttpCache.Items.Messages;

namespace HttpCache.Items
{
    public class InMemoryItemsStoreActor : ReceiveActor
    {
        private readonly List<Item> items;

        public InMemoryItemsStoreActor()
        {
            items = new List<Item>();

            Receive<GetItemRequest>(request => HandleGetItem(request));
            Receive<StoreItem>(message => HandleStoreItem(message));
        }

        private void HandleGetItem(GetItemRequest request)
        {
            var item = items.Single(x => x.Id == request.Id);
            Sender.Tell(new GetItemResponse(item.Id, item.Code, item.Description, item.Value, item.ETag));
        }

        private void HandleStoreItem(StoreItem message)
        {
            var item = new Item
            {
                Id = message.Id,
                Code = message.Code,
                Description = message.Description,
                Value = message.Value,
                ETag = message.ETag
            };

            items.Add(item);
        }

        private class Item
        {
            public int Id { get; set; }
            public int Code { get; set;  }
            public string Description { get; set; }
            public double Value { get; set; }
            public string ETag { get; set; }
        }
    }
}