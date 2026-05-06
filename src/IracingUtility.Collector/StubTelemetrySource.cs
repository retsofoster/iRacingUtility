using IracingUtility.Core;

namespace IracingUtility.Collector;

public sealed class StubTelemetrySource : ITelemetrySource
{
    public async IAsyncEnumerable<TelemetrySample> StreamAsync([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var speed = 80.0;
        while (!cancellationToken.IsCancellationRequested)
        {
            speed = Math.Min(260, speed + 0.5);
            yield return new TelemetrySample(
                DateTimeOffset.UtcNow,
                speed,
                0.75,
                0.0,
                4,
                32.1);

            await Task.Delay(250, cancellationToken);
        }
    }
}
