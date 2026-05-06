namespace IracingUtility.Core;

public sealed record TelemetrySample(
    DateTimeOffset Timestamp,
    double SpeedKph,
    double Throttle,
    double Brake,
    int Gear,
    double FuelLiters);
