namespace Cosmos.Samples.Service.Host.Settings;

internal record ServiceConfiguration
{
    public int PingFrequency { get; set; } = 1;
}