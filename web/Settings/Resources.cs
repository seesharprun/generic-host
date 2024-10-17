namespace Cosmos.Samples.Service.Web.Settings;

internal record Resources
{
    public string DatabaseName { get; set; } = String.Empty;

    public string DataContainerName { get; set; } = String.Empty;

    public string LeaseContainerName { get; set; } = String.Empty;

    public string DataContainerPartitionKey { get; set; } = String.Empty;

    public string LeaseContainerPartitionKey { get; set; } = String.Empty;
}