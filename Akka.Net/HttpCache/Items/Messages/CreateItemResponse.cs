namespace HttpCache.Items.Messages
{
    public class CreateItemResponse
    {
        public string Id { get; }
        public string ETag { get; }

        public CreateItemResponse(string id, string eTag)
        {
            ETag = eTag;
            Id = id;
        }
    }
}