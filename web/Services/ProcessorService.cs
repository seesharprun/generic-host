using Cosmos.Samples.Service.Web.Models;

namespace Cosmos.Samples.Service.Web.Services;

internal class ProcessorService(ILogger<ProcessorService> logger, IObserverService observerService) : IHostedService
{
    public async Task StartAsync(CancellationToken _)
    {
        logger.LogInformation("Processor service is starting...");
        await observerService.StartAsync<Order>();
    }

    public async Task StopAsync(CancellationToken _)
    {
        logger.LogInformation("Processor service is stopping...");
        await observerService.StopAsync();
    }
}