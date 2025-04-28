using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sync.Api.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public string? Username { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("passwordHash")]
        public string? PasswordHash { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("lastActive")]
        public DateTime? LastActive { get; set; }
    }
}