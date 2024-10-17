namespace Cosmos.Samples.Service.Web.Settings;

internal record Credentials
{
    public string Endpoint { get; set; } = String.Empty;

    public string ReadWriteKey { get; set; } = String.Empty;
}