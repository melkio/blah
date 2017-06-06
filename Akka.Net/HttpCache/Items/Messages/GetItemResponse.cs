namespace HttpCache.Items.Messages
{
    public class GetItemResponse
    {
        public int Id { get; }
        public int Code { get; }
        public string Description { get; }
        public double Value { get; }
        public string ETag { get; }
        public bool HasBeenModified { get; }
        public bool Exists { get; }

        private GetItemResponse(int id, int code, string description, double value, string eTag, bool hasBeenModified, bool exists)
        {
            Id = id;
            Code = code;
            Description = description;
            Value = value;
            ETag = eTag;
            HasBeenModified = hasBeenModified;
            Exists = exists;
        }

        public static GetItemResponse FromStore(int id, int code, string description, double value, string eTag)
        {
            return new GetItemResponse(id, code, description, value, eTag, true, true);
        }

        public static GetItemResponse HasNotBeenModified(int id, string eTag)
        {
            return new GetItemResponse(id, 0, string.Empty, 0, eTag, false, true);
        }

        public static GetItemResponse DoesNotExist(int id)
        {
            return new GetItemResponse(id, 0, string.Empty, 0, string.Empty, false, false);
        }
    }
}