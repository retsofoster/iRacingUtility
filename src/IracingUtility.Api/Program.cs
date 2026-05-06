using IracingUtility.Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapGet("/api/reference-sample", () =>
{
    var sample = new TelemetrySample(DateTimeOffset.UtcNow, 180.5, 0.82, 0.0, 5, 28.4);
    return Results.Ok(sample);
});

app.Run();
