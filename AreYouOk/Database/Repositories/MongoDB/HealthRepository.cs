using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AreYouOk.Database.Models;
using MongoDB.Driver;

namespace AreYouOk.Database.Repositories.MongoDB
{
    public class HealthRepository : IHealthRepository
    {
        private readonly DataContext _context;
        public HealthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<HealthModel> AddHealthResponse(bool success, int statusCode, int elapsedMilliseconds, string url)
        {
            var healthModel = new MongoHealthModel
            {
                Success = success,
                Timestamp = DateTime.UtcNow,
                Url = url,
                ElapsedMilliseconds = elapsedMilliseconds,
                StatusCode = statusCode
            };

            await _context.HealthCollection.InsertOneAsync(healthModel);
            return healthModel;
        }

        public IEnumerable<HealthModel> GetLatestHealthForDistinctEndpoints()
        {
            var urls = _context.HealthCollection.Find(FilterDefinition<MongoHealthModel>.Empty).ToList().GroupBy(x => x.Url);

            return urls.Select(url => url.OrderByDescending(x => x.Timestamp).FirstOrDefault()).ToList();
        }

        public async Task DeleteLogsOlderBeforeDate(DateTime date)
        {
            await _context.HealthCollection.DeleteManyAsync(doc => doc.Timestamp < date);
        }
    }
}
