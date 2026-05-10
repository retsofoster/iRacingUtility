namespace IracingUtility.Core;

public interface ITelemetryConnectionStatusProvider
{
    TelemetryConnectionState Current { get; }
}
