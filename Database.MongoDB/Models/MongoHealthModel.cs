using System;
using MongoDB.Bson;

namespace Database.MongoDB.Models
{
    public class MongoHealthModel
    {
        public ObjectId Id { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public int ElapsedMilliseconds { get; set; }
        public DateTime Timestamp { get; set; }
        public string Url { get; set; }
    }
}
