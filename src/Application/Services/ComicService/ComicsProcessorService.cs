using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services.ComicService
{
    public sealed class ComicsProcessorService : BackgroundService
    {
        private readonly ILogger<ComicsProcessorService> _logger;

        public ComicsProcessorService(ILogger<ComicsProcessorService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Comics processor background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Comics processor background service stopped.");
        }
    }
}
