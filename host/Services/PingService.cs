namespace Cosmos.Samples.Service.Host.Services;

internal abstract class PingService(ILogger<PingService> logger, ITimerService timerService, IMessageService messageService, string caller, int pingFrequency) : BackgroundService
{
    private Timer? _timer;
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = timerService.GetTimer(DoWorkAsync, pingFrequency);

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

        logger.LogInformation(messageService.GetMessage(caller));
    }
}
