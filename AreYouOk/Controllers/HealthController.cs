﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.InboundPorts;
using Microsoft.AspNetCore.Mvc;

namespace AreYouOk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HealthService _service;

        public HealthController(HealthService service)
        {
            _service = service;
        }

        [HttpGet("latest")]
        public IActionResult GetLatest()
        {
            var health = _service.GetLatestHealth();

            return Ok(health.Select(x => new LatestHealthDTO
            {
                Success = x.Success,
                Timestamp = x.Timestamp,
                Url = x.Url
            }));
        }
    }

    public class LatestHealthDTO
    {
        public DateTime Timestamp { get; set; }
        public bool Success { get; set; }
        public string Url { get; set; }
    }
}
