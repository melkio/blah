namespace HttpCache.Items.Messages
{
    public class GetItemRequest
    {
        public int Id { get; }
        public string ETag { get; }

        public GetItemRequest(int id, string eTag)
        {
            Id = id;
            ETag = eTag;
        }
    }
}