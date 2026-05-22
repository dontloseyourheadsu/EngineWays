namespace EngineWays.Backend.Features.Routing;

public enum TravelMode
{
    Metro,
    Walk,
    Bike
}

public sealed record RoutePointDto(
    string? Name,
    double? Latitude,
    double? Longitude,
    string? StopId
);

public sealed record RouteRequestDto(
    RoutePointDto Origin,
    RoutePointDto Destination,
    IReadOnlyList<RoutePointDto>? Stops,
    TravelMode Mode
);

public sealed record RouteStepDto(
    string Title,
    string Description,
    string Mode,
    int Minutes,
    double DistanceKm
);

public sealed record RoutePlanResponseDto(
    string Status,
    int TotalMinutes,
    double TotalDistanceKm,
    IReadOnlyList<RouteStepDto> Steps
);
