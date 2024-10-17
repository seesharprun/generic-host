using Cosmos.Samples.Service.Host.Services;
using Cosmos.Samples.Service.Host.Settings;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<FirstPingService>();
builder.Services.AddHostedService<SecondPingService>();

builder.Services.AddSingleton<ITimerService, TimerService>();
builder.Services.AddTransient<IMessageService, MessageService>();

builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<PingService>();

builder.Services.Configure<Configuration>(builder.Configuration.GetSection(nameof(Configuration)));

var host = builder.Build();

await host.RunAsync();