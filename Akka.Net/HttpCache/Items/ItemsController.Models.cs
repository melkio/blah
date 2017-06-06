namespace HttpCache.Items
{
    public partial class ItemsController
    {
        public class GetModel
        {
            public int Id { get; set; }
            public int Code { get; set; }
            public string Description { get; set; }
            public double Value { get; set; }
        }

        public class PostModel
        {
            public int Code { get; set; }
            public string Description { get; set; }
            public double Value { get; set; }
        }
    }
}