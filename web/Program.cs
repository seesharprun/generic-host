using Cosmos.Samples.Service.Web.Components;
using Cosmos.Samples.Service.Web.Services;
using Cosmos.Samples.Service.Web.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<Program>();

builder.Services.Configure<Credentials>(builder.Configuration.GetSection(nameof(Credentials)));
builder.Services.Configure<Resources>(builder.Configuration.GetSection(nameof(Resources)));

builder.Services.AddTransient<IObserverService, AzureCosmosDBNoSQLChangeFeedService>();
builder.Services.AddTransient<IDataRepositoryService, AzureCosmosDBNoSQLContainerDataService>();

builder.Services.AddSingleton<INotificationService, ToastNotificationService>();

builder.Services.AddSingleton(async (IServiceProvider provider) =>
{
    Credentials credentials = provider.GetRequiredService<IOptions<Credentials>>().Value;
    Resources resources = provider.GetRequiredService<IOptions<Resources>>().Value;

    CosmosClient client = new CosmosClientBuilder(credentials.Endpoint, credentials.ReadWriteKey)
       .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
       .Build();

    Database database = await client.CreateDatabaseIfNotExistsAsync(resources.DatabaseName);

    await database.CreateContainerIfNotExistsAsync(resources.DataContainerName, resources.DataContainerPartitionKey);
    await database.CreateContainerIfNotExistsAsync(resources.LeaseContainerName, resources.LeaseContainerPartitionKey);

    return client;
});
builder.Services.AddSingleton((IServiceProvider provider) => provider.GetRequiredService<Task<CosmosClient>>().Result);

builder.Services.AddHostedService<ProcessorService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseHttpsRedirection();

app.UseAntiforgery();

await app.RunAsync();
