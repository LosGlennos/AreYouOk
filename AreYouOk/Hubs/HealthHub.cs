using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using AreYouOk.Database.Models;

namespace AreYouOk.Hubs
{
    public class HealthHub : Hub
    {
        public async Task SendHealth(HealthModel model)
        {
            await Clients.All.SendAsync("ReceiveHealth", model.Timestamp, model.Success, model.StatusCode, model.Url, model.ElapsedMilliseconds);
        }
    }
}