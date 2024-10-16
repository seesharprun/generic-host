using Cosmos.Samples.Service.Host.Settings;
using Microsoft.Extensions.Options;

namespace Cosmos.Samples.Service.Host.Services;

internal class PingService(ILogger<PingService> logger, IMessageService messageService, IOptions<ServiceConfiguration> serviceConfigurationOptions) : BackgroundService
{
    private Timer? _timer;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly ServiceConfiguration _serviceConfiguration = serviceConfigurationOptions.Value;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWorkAsync, new { }, TimeSpan.Zero, TimeSpan.FromSeconds(_serviceConfiguration.PingFrequency));

        stoppingToken.Register(() =>
        {
            _cancellationTokenSource.Cancel();
            _timer?.Change(Timeout.Infinite, 0);
        });

        return Task.CompletedTask;
    }

    private async void DoWorkAsync(object? _)
    {
        await Task.Delay(TimeSpan.FromSeconds(2.5));

        var message = messageService.GetMessage();
        logger.LogInformation(message);
    }
}
