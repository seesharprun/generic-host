using Cosmos.Samples.Service.Host.Services;
using Cosmos.Samples.Service.Host.Settings;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<PingService>();

builder.Services.AddSingleton<IMessageService, MessageService>();

builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<PingService>();

builder.Services.Configure<Messages>(builder.Configuration.GetSection(nameof(Messages)));
builder.Services.Configure<ServiceConfiguration>(builder.Configuration.GetSection(nameof(ServiceConfiguration)));

var host = builder.Build();

await host.RunAsync();