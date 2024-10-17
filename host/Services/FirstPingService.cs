using Cosmos.Samples.Service.Host.Settings;
using Microsoft.Extensions.Options;

namespace Cosmos.Samples.Service.Host.Services;

internal sealed class FirstPingService : PingService
{
    public FirstPingService(ILogger<FirstPingService> logger, ITimerService timerService, IMessageService messageService, IOptions<Configuration> serviceConfiguration)
        : base(logger, timerService, messageService, nameof(FirstPingService), serviceConfiguration.Value.FirstPingFrequency)
    { }
}