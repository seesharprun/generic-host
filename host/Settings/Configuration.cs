namespace Cosmos.Samples.Service.Host.Settings;

internal record Configuration
{
    public int FirstPingFrequency { get; set; } = 1;
    public int SecondPingFrequency { get; set; } = 1;
}