using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AreYouOk.Database.Models;
using AreYouOk.Database.Repositories;

namespace AreYouOk.Data
{
    public class HealthService
    {
        private readonly IHealthRepository _repository;

        public HealthService(IHealthRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<HealthModel> GetLatestHealth()
        {
            return _repository.GetLatestHealthForDistinctEndpoints();
        }

        public async Task<HealthModel> AddHealthResponse(bool isSuccess, int statusCode, int elapsedMilliseconds, string url) {
            return await _repository.AddHealthResponse(isSuccess, statusCode, elapsedMilliseconds, url);
        }

        public async Task DeleteLogsOlderThanDays(int days)
        {
            var date = DateTime.UtcNow.AddDays(-days);
            await _repository.DeleteLogsOlderBeforeDate(date);
        }
    }
}
