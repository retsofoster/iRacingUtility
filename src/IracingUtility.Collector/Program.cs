using IracingUtility.Collector;
using IracingUtility.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IracingMemoryMappedTelemetrySource>();
builder.Services.AddSingleton<ITelemetrySource>(sp => sp.GetRequiredService<IracingMemoryMappedTelemetrySource>());
builder.Services.AddSingleton<ITelemetryConnectionStatusProvider>(sp => sp.GetRequiredService<IracingMemoryMappedTelemetrySource>());
builder.Services.AddHostedService<CollectorWorker>();

await builder.Build().RunAsync();
