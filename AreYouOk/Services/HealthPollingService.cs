using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AreYouOk.Data;
using AreYouOk.Hubs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AreYouOk.Services
{
    public class HealthPollingService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly HttpClient _client;

        public HealthPollingService(IServiceProvider services, IConfiguration configuration, IHttpClientFactory client, ILogger logger)
        {
            _services = services;
            _configuration = configuration;
            _logger = logger;
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
            using var scope = _services.CreateScope();

            try
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

                var healthModel = await service.AddHealthResponse(isSuccess, statusCode, elapsedMilliseconds, url);
                await hub.SendHealth(healthModel);
                var retentionDays = Convert.ToInt32(_configuration["DATA_RETENTION_DAYS"]);
                await service.DeleteLogsOlderThanDays(retentionDays);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error occurred: {ErrorMessage}", e.Message);
            }
        }
    }
}