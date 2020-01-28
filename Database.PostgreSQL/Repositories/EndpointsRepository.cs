using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.OutboundPorts.Repositories;
using Database.PostgreSQL.Models;

namespace Database.PostgreSQL.Repositories
{
    public class EndpointsRepository : IEndpointsRepository
    {
        private readonly DataContext _dataContext;

        public EndpointsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddEndpoint(string endpoint)
        {
            var endpointModel = new EndpointModel { Endpoint = endpoint };
            _dataContext.Add(endpointModel);
            await _dataContext.SaveChangesAsync();
        }

        public List<string> GetEndpoints()
        {
            return _dataContext.Endpoints.Select(x => x.Endpoint).ToList();
        }

        public async Task DeleteEndpoint(string endpoint)
        {
            var endpointToRemove = _dataContext.Endpoints.Single(e => e.Endpoint == endpoint);
            _dataContext.Endpoints.Remove(endpointToRemove);
            await _dataContext.SaveChangesAsync();
        }
    }
}