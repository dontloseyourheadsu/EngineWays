namespace EngineWays.Backend.Features.Stops;

public sealed record StopSearchResultDto(
    string StopId,
    string StopName,
    double StopLat,
    double StopLon,
    string? StopCode
);
