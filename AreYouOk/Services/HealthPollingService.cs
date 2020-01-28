using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.InboundPorts;
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
            await Task.Delay(10000);
            using var scope = _services.CreateScope();
            var endpointsService = scope.ServiceProvider.GetRequiredService<EndpointsService>();
            var waitTime = Convert.ToInt32(_configuration["HEALTH_POLL_RATE_SECONDS"]);
            while (!cancellationToken.IsCancellationRequested)
            {
                var urls = endpointsService.GetEndpoints();
                foreach (var url in urls)
                {
                    await PingService(scope, url);
                }

                await Task.Delay(waitTime * 1000);
            }
        }

        public async Task PingService(IServiceScope scope, string url)
        {
            try
            {
                var service = scope.ServiceProvider.GetRequiredService<HealthService>();
                var timer = new Stopwatch();
                timer.Start();
                var response = await _client.GetAsync(url);
                timer.Stop();

                var elapsedMilliseconds = (int)timer.ElapsedMilliseconds;
                var isSuccess = response.IsSuccessStatusCode;
                var statusCode = (int)response.StatusCode;

                var healthModel = await service.AddHealthResponse(isSuccess, statusCode, elapsedMilliseconds, url);
                await service.SendHealth(healthModel);
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