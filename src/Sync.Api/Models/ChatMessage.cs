using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sync.Api.Models
{
    public class ChatMessage
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("senderId")]
        public string? SenderId { get; set; }

        [BsonElement("receiverId")]
        public string? ReceiverId { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("isRead")]
        public bool IsRead { get; set; }
    }
}