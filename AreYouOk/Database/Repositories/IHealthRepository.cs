using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AreYouOk.Database.Models;

namespace AreYouOk.Database.Repositories {
    public interface IHealthRepository {
        Task<HealthModel> AddHealthResponse(bool success, int statusCode, int elapsedMilliseconds, string url);
        IEnumerable<HealthModel> GetLatestHealthForDistinctEndpoints();
        Task DeleteLogsOlderBeforeDate(DateTime date);
    }
}