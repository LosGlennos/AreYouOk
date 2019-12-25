using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.OutboundPorts.Repositories;
using Database.PostgreSQL.Models;

namespace Database.PostgreSQL
{
    public class HealthRepository : IHealthRepository
    {
        private readonly DataContext _dataContext;

        public HealthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Core.Models.HealthModel> AddHealthResponse(bool success, int statusCode, int elapsedMilliseconds, string url)
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

            return new Core.Models.HealthModel
            {
                Timestamp = healthModel.Timestamp,
                Success = healthModel.Success,
                Url = healthModel.Url,
                ElapsedMilliseconds = healthModel.ElapsedMilliseconds,
                StatusCode = healthModel.StatusCode
            };
        }

        public IEnumerable<Core.Models.HealthModel> GetLatestHealthForDistinctEndpoints()
        {
            var urls = _dataContext.HealthData.ToList().GroupBy(x => x.Url);

            return urls.Select(url => url.OrderByDescending(x => x.Timestamp).FirstOrDefault()).Select(x => new Core.Models.HealthModel
            {
                Success = x.Success,
                Url = x.Url,
                Timestamp = x.Timestamp,
                ElapsedMilliseconds = x.ElapsedMilliseconds,
                StatusCode = x.StatusCode
            }).ToList();
        }

        public async Task DeleteLogsOlderBeforeDate(DateTime date)
        {
            var oldData = _dataContext.HealthData.Where(x => x.Timestamp < date).ToList();
            _dataContext.HealthData.RemoveRange(oldData);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AddEndpoint(string endpoint)
        {
            var endpointModel = new EndpointModel { Endpoint = endpoint };
            _dataContext.Add(endpointModel);
            await _dataContext.SaveChangesAsync();
        }

        public List<string> GetEndpoints()
        {
            return _dataContext.Endpoints.Select(x => x.Endpoint).ToList();
        }
    }
}