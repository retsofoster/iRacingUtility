using System.IO.MemoryMappedFiles;
using IracingUtility.Core;

namespace IracingUtility.Collector;

/// <summary>
/// Reads iRacing connection availability from the official shared memory map name.
/// Produces telemetry samples only when connected.
/// </summary>
public sealed class IracingMemoryMappedTelemetrySource : ITelemetrySource, ITelemetryConnectionStatusProvider
{
    private const string IracingMapName = "Local\\IRSDKMemMapFileName";

    public TelemetryConnectionState Current { get; private set; } =
        new(false, DateTimeOffset.UtcNow, "iRacing shared memory");

    public async IAsyncEnumerable<TelemetrySample> StreamAsync([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var connected = IsIracingConnected();
            Current = new TelemetryConnectionState(connected, DateTimeOffset.UtcNow, "iRacing shared memory");

            if (connected)
            {
                // TODO: Parse real telemetry variables from shared memory pages.
                // For now, this confirms a live iRacing connection and emits heartbeat samples.
                yield return new TelemetrySample(
                    DateTimeOffset.UtcNow,
                    0,
                    0,
                    0,
                    0,
                    0);
            }

            await Task.Delay(250, cancellationToken);
        }
    }

    private static bool IsIracingConnected()
    {
        try
        {
            using var mmf = MemoryMappedFile.OpenExisting(IracingMapName, MemoryMappedFileRights.Read);
            using var view = mmf.CreateViewStream(0, 16, MemoryMappedFileAccess.Read);
            return view.Length > 0;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
