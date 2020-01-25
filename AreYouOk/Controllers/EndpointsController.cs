using Core.InboundPorts;
using Microsoft.AspNetCore.Mvc;

namespace AreYouOk.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult AddEnpoint()
        {
            return Ok();
        }
    }
}