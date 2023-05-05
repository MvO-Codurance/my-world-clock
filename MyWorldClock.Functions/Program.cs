using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWorldClock;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        
        services
            .AddSingleton<IClock>(_ => SystemClock.Instance)
            .AddScoped<IClockService, ClockService>()
            .AddScoped<IWorldClockService, WorldClockService>()
            .AddScoped<ILanguageService, LanguageService>();
    })
    .Build();

await host.RunAsync();