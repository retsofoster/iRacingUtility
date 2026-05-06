using IracingUtility.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<ITelemetrySource, StubTelemetrySource>();
builder.Services.AddHostedService<CollectorWorker>();

await builder.Build().RunAsync();
