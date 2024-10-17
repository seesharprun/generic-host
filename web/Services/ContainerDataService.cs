using Cosmos.Samples.Service.Web.Models;
using Cosmos.Samples.Service.Web.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Cosmos.Samples.Service.Web.Services;

internal interface IDataRepositoryService
{
    Task<IEnumerable<T>> GetAllAsync<T>() where T : Item;

    Task<T> AddOrReplaceAsync<T>(T item) where T : Item;

    Task DeleteAsync<T>(T item, Func<T, string> getPartitionKey) where T : Item;
}

internal sealed class AzureCosmosDBNoSQLContainerDataService(ILogger<AzureCosmosDBNoSQLContainerDataService> logger, IOptions<Resources> resourcesSettings, CosmosClient client) : IDataRepositoryService
{
    private readonly Container _dataContainer = client.GetContainer(resourcesSettings.Value.DatabaseName, resourcesSettings.Value.DataContainerName);

    public async Task<IEnumerable<T>> GetAllAsync<T>() where T : Item
    {
        logger.LogInformation($"Querying container [{_dataContainer.Id}] for all items...");

        var query = _dataContainer.GetItemQueryIterator<T>();

        List<T> results = new List<T>();

        while (query.HasMoreResults)
        {
            results.AddRange(await query.ReadNextAsync());
        }

        return results;
    }

    public async Task<T> AddOrReplaceAsync<T>(T item) where T : Item
    {
        logger.LogInformation($"Upserting item\t[{item.id}]");

        T result = await _dataContainer.UpsertItemAsync(item);

        return result;
    }

    public async Task DeleteAsync<T>(T item, Func<T, string> getPartitionKey) where T : Item
    {
        logger.LogInformation($"Deleting item\t[{item.id}]");

        PartitionKey partitionKey = new(getPartitionKey(item));
        await _dataContainer.DeleteItemAsync<T>(item.id, partitionKey);
    }
}