using System.Collections.Generic;
using AreYouOk.Data;
using AreYouOk.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace AreYouOk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController
    {
        private readonly HealthService _service;

        public HealthController(HealthService service)
        {
            _service = service;
        }

        [HttpGet("latest")]
        public IEnumerable<HealthModel> GetLatest()
        {
            return _service.GetLatestHealth();
        }
    }

    public class HealthDTO
    {
    }
}
