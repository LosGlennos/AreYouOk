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
        public async Task AddHealthResponse(bool successfull, int statusCode, int elapsedMilliseconds)
        {
            _dataContext.Add(new HealthModel
            {
                ElapsedMilliseconds = elapsedMilliseconds,
                StatusCode = statusCode,
                Success = successfull,
                Timestamp = DateTime.UtcNow
            });

            await _dataContext.SaveChangesAsync();
        }

        public IEnumerable<HealthModel> GetHealth()
        {
            return _dataContext.HealthData.ToList();
        }
    }
}