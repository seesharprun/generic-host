using Cosmos.Samples.Service.Web.Models;
using Cosmos.Samples.Service.Web.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Cosmos.Samples.Service.Web.Services;

internal interface IObserverService
{
    Task StartAsync<T>(Func<T, string>? getItemDisplayName = null) where T : Item;
    Task StopAsync();
}

internal sealed class AzureCosmosDBNoSQLChangeFeedService(ILogger<AzureCosmosDBNoSQLChangeFeedService> logger, IOptions<Resources> resourcesSettings, INotificationService notificationService, CosmosClient client) : IObserverService
{
    private readonly Container _dataContainer = client.GetContainer(resourcesSettings.Value.DatabaseName, resourcesSettings.Value.DataContainerName);
    
    private readonly Container _leaseContainer = client.GetContainer(resourcesSettings.Value.DatabaseName, resourcesSettings.Value.LeaseContainerName);

    private ChangeFeedProcessor? _changeFeedProcessor;

    public async Task StartAsync<T>(Func<T, string>? getItemDisplayName = null) where T : Item
    {
        _changeFeedProcessor = _dataContainer
            .GetChangeFeedProcessorBuilder<T>("processorName", async (changes, _) => await HandleChangesAsync(changes, getItemDisplayName))
            .WithInstanceName($"instance-{Guid.NewGuid()}")
            .WithLeaseContainer(_leaseContainer)
            .Build();

        await _changeFeedProcessor.StartAsync();
    }

    private async Task HandleChangesAsync<T>(IReadOnlyCollection<T> changes, Func<T, string>? getItemDisplayName) where T : Item
    {
        foreach (var item in changes)
        {
            logger.LogInformation($"Detected operation for item\t[{item.id}]");

            notificationService.Send($"Change feed picked up item: {(getItemDisplayName is not null ? getItemDisplayName(item) : item.id)}");
        }
        await Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        if (_changeFeedProcessor is not null)
        {
            await _changeFeedProcessor.StopAsync();
        }
    }
}