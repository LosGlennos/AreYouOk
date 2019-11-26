using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.OutboundPorts.Notifications;
using Core.OutboundPorts.Repositories;

namespace Core.InboundPorts
{
    public class HealthService
    {
        private readonly IHealthRepository _repository;
        private readonly IHealthNotification _healthHub;

        public HealthService(IHealthRepository repository, IHealthNotification healthHub)
        {
            _repository = repository;
            _healthHub = healthHub;
        }

        public async Task SendHealth(HealthModel healthModel)
        {
            await _healthHub.SendHealth(healthModel);
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
