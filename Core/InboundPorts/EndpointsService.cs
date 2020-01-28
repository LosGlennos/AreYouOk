using System.Collections.Generic;
using System.Threading.Tasks;
using Core.OutboundPorts.Repositories;

namespace Core.InboundPorts
{
    public class EndpointsService
    {
        private readonly IEndpointsRepository _endpointsRepository;

        public EndpointsService(IEndpointsRepository endpointsRepository)
        {
            _endpointsRepository = endpointsRepository;
        }

        public async Task AddEndpoint(string endpoint)
        {
            await _endpointsRepository.AddEndpoint(endpoint);
        }

        public List<string> GetEndpoints() {
            return _endpointsRepository.GetEndpoints();
        }

        public async Task RemoveEndpoint(string endpoint)
        {
            await _endpointsRepository.DeleteEndpoint(endpoint);
        }
    }
}