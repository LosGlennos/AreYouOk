using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.OutboundPorts.Repositories;
using Database.MongoDB.Models;
using MongoDB.Driver;

namespace Database.MongoDB.Repositories
{
    public class EndpointsRepository : IEndpointsRepository
    {
        private readonly DataContext _context;
        public EndpointsRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task AddEndpoint(string endpoint)
        {
            var endpointModel = new EndpointModel { Endpoint = endpoint };
            
            await _context.EndpointCollection.InsertOneAsync(endpointModel);
        }

        public List<string> GetEndpoints()
        {
            var endpoints = _context.EndpointCollection.Find(FilterDefinition<Models.EndpointModel>.Empty).ToList();

            return endpoints.Select(endpoint => endpoint.Endpoint).ToList();
        }
    }
}