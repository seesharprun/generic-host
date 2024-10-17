using Cosmos.Samples.Service.Host.Settings;
using Microsoft.Extensions.Options;

namespace Cosmos.Samples.Service.Host.Services;

internal sealed class SecondPingService : PingService
{
    public SecondPingService(ILogger<FirstPingService> logger, ITimerService timerService, IMessageService messageService, IOptions<Configuration> serviceConfiguration)
        : base(logger, timerService, messageService, nameof(SecondPingService), serviceConfiguration.Value.SecondPingFrequency)
    { }
}