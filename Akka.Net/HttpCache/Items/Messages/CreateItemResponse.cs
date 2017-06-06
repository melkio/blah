namespace HttpCache.Items.Messages
{
    public class CreateItemResponse
    {
        public int Id { get; }
        public string ETag { get; }

        public CreateItemResponse(int id, string eTag)
        {
            ETag = eTag;
            Id = id;
        }
    }
}