using System;
using System.Threading.Tasks;
using Core.InboundPorts;
using Microsoft.AspNetCore.Mvc;

namespace AreYouOk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EndpointsController : ControllerBase
    {
        private readonly EndpointsService _endpointsService;

        public EndpointsController(EndpointsService endpointsService)
        {
            _endpointsService = endpointsService;
        }

        [HttpGet]
        public IActionResult GetEndpoints()
        {
            return Ok(_endpointsService.GetEndpoints());
        }

        [HttpPost]
        public async Task<IActionResult> AddEndpoint(EndpointDto endpoint)
        {
            await _endpointsService.AddEndpoint(endpoint.Endpoint);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveEndpoint(EndpointDto endpoint)
        {
            await _endpointsService.RemoveEndpoint(endpoint.Endpoint);
            return Ok();
        }
    }

    public class EndpointDto
    {
        public string Endpoint { get; set; }
    }
}