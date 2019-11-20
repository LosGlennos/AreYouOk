using System.Collections.Generic;
using System.Threading.Tasks;
using AreYouOk.Database.Models;

namespace AreYouOk.Database.Repositories {
    public interface IHealthRepository {
        Task AddHealthResponse(bool successfull, int statusCode, int elapsedMilliseconds);
        IEnumerable<HealthModel> GetHealth();
    }
}