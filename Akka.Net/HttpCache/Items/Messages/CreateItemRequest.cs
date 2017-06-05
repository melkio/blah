namespace HttpCache.Items.Messages
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
}