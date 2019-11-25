using System;
using System.Collections.Generic;
using System.Linq;
using AreYouOk.Data;
using AreYouOk.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AreYouOk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController
    {
        private readonly HealthService _service;
        private readonly ILogger _logger;

        public HealthController(HealthService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
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
