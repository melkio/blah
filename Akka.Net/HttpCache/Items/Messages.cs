namespace HttpCache.Items
{
    public class CreateItemRequest
    {
        public int Code { get; }
        public string Description { get; }
        public double Value { get; }

        public CreateItemRequest(int code, string description, double value)
        {
            Code = code;
            Description = description;
            Value = value;
        }
    }

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