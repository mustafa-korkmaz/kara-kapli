using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dal.Entities
{
    public class File
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string Name { get; set; }

        public byte[] Content { get; set; }
    }
}