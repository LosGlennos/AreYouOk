using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.OutboundPorts.Repositories;
using Database.MongoDB.Models;
using MongoDB.Driver;

namespace Database.MongoDB.Repositories
{
    public class HealthRepository : IHealthRepository
    {
        private readonly DataContext _context;
        public HealthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Core.Models.HealthModel> AddHealthResponse(bool success, int statusCode, int elapsedMilliseconds, string url)
        {
            var healthModel = new Models.HealthModel
            {
                Success = success,
                Timestamp = DateTime.UtcNow,
                Url = url,
                ElapsedMilliseconds = elapsedMilliseconds,
                StatusCode = statusCode
            };

            if (_context.HealthCollection == null)
                throw new NullReferenceException("Database not initialized");

            await _context.HealthCollection.InsertOneAsync(healthModel);
            return new Core.Models.HealthModel
            {
                StatusCode = healthModel.StatusCode,
                ElapsedMilliseconds = healthModel.ElapsedMilliseconds,
                Timestamp = healthModel.Timestamp,
                Success = healthModel.Success,
                Url = healthModel.Url
            };

        }

        public IEnumerable<Core.Models.HealthModel> GetLatestHealthForDistinctEndpoints()
        {
            var urls = _context.HealthCollection.Find(FilterDefinition<Models.HealthModel>.Empty).ToList().GroupBy(x => x.Url);

            return urls.Select(url => url.OrderByDescending(x => x.Timestamp).FirstOrDefault()).Select(x => new Core.Models.HealthModel
            {
                Url = x.Url,
                Timestamp = x.Timestamp,
                Success = x.Success,
                ElapsedMilliseconds = x.ElapsedMilliseconds,
                StatusCode = x.StatusCode
            }).ToList();
        }

        public async Task DeleteLogsOlderBeforeDate(DateTime date)
        {
            await _context.HealthCollection.DeleteManyAsync(doc => doc.Timestamp < date);
        }
    }
}
