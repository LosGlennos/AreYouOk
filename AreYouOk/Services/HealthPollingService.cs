using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AreYouOk.Data;
using AreYouOk.Hubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AreYouOk.Services
{
    public class HealthPollingService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public HealthPollingService(IServiceProvider services, IConfiguration configuration, IHttpClientFactory client)
        {
            _services = services;
            _configuration = configuration;
            _client = client.CreateClient();
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var urls = _configuration["HEALTH_ENDPOINTS"].Split(' ');
            var waitTime = Convert.ToInt32(_configuration["HEALTH_POLL_RATE_SECONDS"]);
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var url in urls)
                {
                    await PingService(url);
                }

                await Task.Delay(waitTime * 1000);
            }
        }

        public async Task PingService(string url)
        {
            using (var scope = _services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<HealthService>();
                var hub = scope.ServiceProvider.GetRequiredService<HealthHub>();
                var timer = new Stopwatch();
                timer.Start();
                var response = await _client.GetAsync(url);
                timer.Stop();

                var elapsedMilliseconds = (int)timer.ElapsedMilliseconds;
                var isSuccess = response.IsSuccessStatusCode;
                var statusCode = (int)response.StatusCode;

                try
                {
                    var healthModel = await service.AddHealthResponse(isSuccess, statusCode, elapsedMilliseconds, url);
                    await hub.SendHealth(healthModel);
                }
                catch (Exception e)
                {
                    var a = e;
                }
            }
        }
    }
}