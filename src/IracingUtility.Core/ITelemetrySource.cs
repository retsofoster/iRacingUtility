namespace IracingUtility.Core;

public interface ITelemetrySource
{
    IAsyncEnumerable<TelemetrySample> StreamAsync(CancellationToken cancellationToken);
}
