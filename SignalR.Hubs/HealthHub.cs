using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace SignalR.Hubs
{
    public class HealthHub : Hub
    {
        private readonly ILogger _logger;

        public HealthHub(ILogger logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.Information("Client connected on connection id {ConnectionId}", Context.ConnectionId);
        }
    }
}