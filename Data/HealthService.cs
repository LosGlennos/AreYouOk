using System.Collections.Generic;
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

        public IEnumerable<HealthModel> GetHealth()
        {
            return _repository.GetHealth();
        }
    }
}
