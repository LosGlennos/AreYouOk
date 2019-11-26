using Database.MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Database.MongoDB
{
    public class DataContext
    {
        private readonly string _connectionString;
        public IMongoCollection<MongoHealthModel> HealthCollection { get; private set; }
        
        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Migrate()
        {
            var lastIndex = _connectionString.LastIndexOf('/');
            var url = _connectionString.Substring(0, lastIndex);
            var databaseName = _connectionString.Substring(lastIndex + 1, _connectionString.Length - lastIndex - 1);

            var client = new MongoClient(url);
            var database = client.GetDatabase(databaseName);

            const string collectionName = "health_data";

            var exists = database.ListCollections(new ListCollectionsOptions {Filter = new BsonDocument("name", collectionName)}).Any();
            if (!exists)
            {
                database.CreateCollection(collectionName);
            }

            HealthCollection = database.GetCollection<MongoHealthModel>(collectionName);
        }
    }
}
