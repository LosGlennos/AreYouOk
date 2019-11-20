using System;
using System.Linq;
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

        public Task<HealthModel[]> GetHealthAsync()
        {
            _repository.GetHealth();
        }
    }
}
