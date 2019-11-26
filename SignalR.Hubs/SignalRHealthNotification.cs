using System;
using Core.Models;
using Core.OutboundPorts.Notifications;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace SignalR.Hubs
{
    public class SignalRHealthNotification : IHealthNotification
    {
        private readonly IHubContext<HealthHub> _hubContext;
        private readonly ILogger _logger;

        public SignalRHealthNotification(IHubContext<HealthHub> hubContext, ILogger logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task SendHealth(HealthModel model)
        {
            if (_hubContext.Clients == null)
            {
                _logger.Information("No clients connected to hub.");
            }
            else
            {
                await _hubContext.Clients.All.SendAsync("ReceiveHealth", model.Timestamp, model.Success, model.StatusCode, model.Url, model.ElapsedMilliseconds);
            }
        }
    }
}
