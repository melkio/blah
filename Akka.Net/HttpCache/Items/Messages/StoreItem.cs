﻿namespace HttpCache.Items.Messages
{
    public class StoreItem
    {
        public int Id { get; }
        public int Code { get; }
        public string Description { get; }
        public double Value { get; }
        public string ETag { get; }

        public StoreItem(int id, int code, string description, double value, string eTag)
        {
            Id = id;
            Code = code;
            Description = description;
            Value = value;
            ETag = eTag;
        }
    }
}