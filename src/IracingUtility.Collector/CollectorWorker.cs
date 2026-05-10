using IracingUtility.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IracingUtility.Collector;

public sealed class CollectorWorker(
    ITelemetrySource telemetrySource,
    ITelemetryConnectionStatusProvider statusProvider,
    ILogger<CollectorWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var lastState = false;

        while (!stoppingToken.IsCancellationRequested)
        {
            var currentState = statusProvider.Current.IsConnected;
            if (currentState != lastState)
            {
                logger.LogInformation("iRacing connection state changed: connected={Connected}", currentState);
                lastState = currentState;
            }

            await foreach (var sample in telemetrySource.StreamAsync(stoppingToken))
            {
                logger.LogInformation(
                    "Telemetry @ {Timestamp}: speed={SpeedKph}kph throttle={Throttle} brake={Brake} gear={Gear} fuel={FuelLiters}",
                    sample.Timestamp,
                    sample.SpeedKph,
                    sample.Throttle,
                    sample.Brake,
                    sample.Gear,
                    sample.FuelLiters);
            }

            await Task.Delay(250, stoppingToken);
        }
    }
}
