using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.OutboundPorts.Notifications
{
    public interface IHealthNotification
    {
        Task SendHealth(HealthModel model);
    }
}
