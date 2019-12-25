using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.OutboundPorts.Repositories 
{
    public interface IEndpointsRepository
    {
        Task AddEndpoint(string endpoint);
        List<string> GetEndpoints();
    }
}