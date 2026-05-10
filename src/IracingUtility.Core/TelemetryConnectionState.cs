namespace IracingUtility.Core;

public sealed record TelemetryConnectionState(bool IsConnected, DateTimeOffset CheckedAtUtc, string Source);
