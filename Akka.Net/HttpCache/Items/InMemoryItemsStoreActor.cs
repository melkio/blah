using System.Collections.Generic;
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

            Receive<StoreItem>(message => HandleStoreItem(message));
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
            public string Id { get; set; }
            public int Code { get; set;  }
            public string Description { get; set; }
            public double Value { get; set; }
            public string ETag { get; set; }
        }
    }
}