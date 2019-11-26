using System;
using System.Collections.Generic;
using System.Linq;
using Core.InboundPorts;
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
        public IEnumerable<LatestHealthDTO> GetLatest()
        {
            var health = _service.GetLatestHealth();

            return health.Select(x => new LatestHealthDTO
            {
                Success = x.Success,
                Timestamp = x.Timestamp,
                Url = x.Url
            });
        }
    }

    public class LatestHealthDTO
    {
        public DateTime Timestamp { get; set; }
        public bool Success { get; set; }
        public string Url { get; set; }
    }
}
