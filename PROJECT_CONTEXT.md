# Project Context for iRacingUtility

## What this project is

`iRacingUtility` is a C#/.NET starter for building an iRacing telemetry utility.
It is structured to evolve into features like:
- real-time telemetry ingestion,
- lap/session persistence,
- lap comparison and delta analytics,
- coaching-style insights.

## Current architecture

- `src/IracingUtility.Core`
  - Shared domain contracts and telemetry models.
  - Contains `TelemetrySample` and `ITelemetrySource`.
- `src/IracingUtility.Collector`
  - Background worker host for telemetry ingestion.
  - Currently wired to a stub telemetry source.
- `src/IracingUtility.Api`
  - ASP.NET Core API host.
  - Starter endpoints for health and sample payload checks.

## Implemented now

- Pluggable telemetry contract (`ITelemetrySource`).
- Synthetic source (`StubTelemetrySource`) for local iteration.
- Worker (`CollectorWorker`) that consumes telemetry and logs samples.
- Minimal API endpoints:
  - `GET /health`
  - `GET /api/reference-sample`

## Not implemented yet

- Real iRacing SDK adapter.
- Storage model for sessions/laps/samples.
- Analytics endpoints (lap delta, corner-level comparisons).
- Frontend dashboard.
- Broad automated tests.

## Suggested immediate next step

Implement a real telemetry adapter in `IracingUtility.Collector` and replace `StubTelemetrySource` behind `ITelemetrySource`, with clear disconnected-state handling when iRacing is not running.

## Reusable prompt for future assistant sessions

```text
You are helping me build a C#/.NET iRacing telemetry utility in repo iRacingUtility.

Architecture:
- src/IracingUtility.Core: shared models/interfaces.
- src/IracingUtility.Collector: telemetry worker.
- src/IracingUtility.Api: ASP.NET Core API.

Implemented:
- TelemetrySample + ITelemetrySource.
- StubTelemetrySource.
- CollectorWorker logging sample stream.
- GET /health and GET /api/reference-sample.

Missing:
- Real iRacing SDK integration.
- Persistence for sessions/laps/samples.
- Lap comparison/delta analytics.
- Frontend and robust tests.

Please propose the smallest valuable next step, implement it cleanly, and include tests when practical.
```
