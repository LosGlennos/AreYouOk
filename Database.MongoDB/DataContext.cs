using System.Collections.Generic;
using System.Linq;
using Database.MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Database.MongoDB
{
    public class DataContext
    {
        private readonly string _connectionString;
        public IMongoCollection<HealthModel> HealthCollection { get; private set; }
        public IMongoCollection<EndpointModel> EndpointCollection {get; private set;}
        
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
            
            var healthCollectionName = "health_data";
            var endpointsCollectionName = "endpoints";
            var collections = new List<string>{healthCollectionName, endpointsCollectionName};

            collections.ForEach(collection => 
            {
                var exists = database.ListCollections(new ListCollectionsOptions {Filter = new BsonDocument("name", collection)}).Any();
                if (!exists)
                {
                    database.CreateCollection(collection);
                }
            });

            HealthCollection = database.GetCollection<HealthModel>(healthCollectionName);
            EndpointCollection = database.GetCollection<EndpointModel>(endpointsCollectionName);
        }
    }
}
