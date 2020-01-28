using MongoDB.Bson;

namespace Database.MongoDB.Models
{
    public class EndpointModel
    {
        public ObjectId Id { get; set; }
        public string Endpoint { get; set; }
    }
}