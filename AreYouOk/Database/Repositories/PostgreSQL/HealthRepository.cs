using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AreYouOk.Database.Models;

namespace AreYouOk.Database.Repositories.PostgreSQL
{
    public class HealthRepository : IHealthRepository
    {
        private readonly DataContext _dataContext;

        public HealthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<HealthModel> AddHealthResponse(bool success, int statusCode, int elapsedMilliseconds, string url)
        {
            var healthModel = new HealthModel
            {
                ElapsedMilliseconds = elapsedMilliseconds,
                StatusCode = statusCode,
                Success = success,
                Timestamp = DateTime.UtcNow,
                Url = url
            };

            _dataContext.Add(healthModel);

            await _dataContext.SaveChangesAsync();

            return healthModel;
        }

        public IEnumerable<HealthModel> GetLatestHealthForDistinctEndpoints()
        {
            var urls = _dataContext.HealthData.ToList().GroupBy(x => x.Url);

            return urls.Select(url => url.OrderByDescending(x => x.Timestamp).FirstOrDefault()).ToList();
        }

        public async Task DeleteLogsOlderBeforeDate(DateTime date)
        {
            var oldData = _dataContext.HealthData.Where(x => x.Timestamp < date).ToList();
            _dataContext.HealthData.RemoveRange(oldData);
            await _dataContext.SaveChangesAsync();
        }
    }
}