using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.OutboundPorts.Repositories 
{
    public interface IHealthRepository {
        Task<HealthModel> AddHealthResponse(bool success, int statusCode, int elapsedMilliseconds, string url);
        IEnumerable<HealthModel> GetLatestHealthForDistinctEndpoints();
        Task DeleteLogsOlderBeforeDate(DateTime date);
        Task AddEndpoint(string endpoint);
        List<string> GetEndpoints();
    }
}