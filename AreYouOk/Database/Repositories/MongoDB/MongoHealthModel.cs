using AreYouOk.Database.Models;
using MongoDB.Bson;

namespace AreYouOk.Database.Repositories.MongoDB
{
    public class MongoHealthModel : HealthModel
    {
        public ObjectId id { get; set; }
    }
}
