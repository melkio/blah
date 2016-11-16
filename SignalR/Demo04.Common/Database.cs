using Demo04.Common.Commands;
using Demo04.Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;

namespace Demo04.Common
{
    public static class Database
    {
        private static readonly IMongoDatabase database;

        public static IMongoCollection<Room> Rooms => database.GetCollection<Room>("rooms");
        public static IMongoCollection<ChatMessage> ChatMessages => database.GetCollection<ChatMessage>("chatMessages");
        public static IMongoCollection<CommandDescriptor> Commands => database.GetCollection<CommandDescriptor>("commands");

        public static IMongoCollection<T> GetCollection<T>(string name)
        {
            return database.GetCollection<T>(name);
        }

        static Database()
        {
            BsonClassMap.RegisterClassMap<Room>(config =>
            {
                config.AutoMap();
                config.GetMemberMap(x => x.Id).SetIdGenerator(new StringObjectIdGenerator());
            });

            BsonClassMap.RegisterClassMap<ChatMessage>(config =>
            {
                config.AutoMap();
                config.GetMemberMap(x => x.Id).SetIdGenerator(new StringObjectIdGenerator());
                config.GetMemberMap(x => x.Date).SetSerializer(new DateTimeSerializer(DateTimeKind.Utc, BsonType.String));
            });

            BsonClassMap.RegisterClassMap<CommandDescriptor>(config =>
            {
                config.AutoMap();
                config.GetMemberMap(x => x.OccurredOn).SetSerializer(new DateTimeSerializer(DateTimeKind.Utc, BsonType.String));
            });

            var client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("ugidotnet");
        }
    }
}
