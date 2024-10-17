namespace Cosmos.Samples.Service.Host.Services;

internal interface ITimerService
{
    Timer GetTimer(TimerCallback callback, int periodInSeconds);
}

internal sealed class TimerService : ITimerService
{
    public Timer GetTimer(TimerCallback callback, int periodInSeconds) =>
        new Timer(callback, null, TimeSpan.FromSeconds(periodInSeconds), TimeSpan.FromSeconds(periodInSeconds));
}