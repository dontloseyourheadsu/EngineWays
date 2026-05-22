namespace EngineWays.Backend.Features.Routing;

public interface IRoutingEngine
{
    Task<RoutePlanResponseDto> PlanAsync(RouteRequestDto request, CancellationToken cancellationToken);
}

public sealed class StubRoutingEngine : IRoutingEngine
{
    public Task<RoutePlanResponseDto> PlanAsync(RouteRequestDto request, CancellationToken cancellationToken)
    {
        var steps = new List<RouteStepDto>
        {
            new("Walk 5 min", "To Metro Insurgentes", "walk", 5, 0.6),
            new("Metro Line 1", "Direction Pantitlan - 4 stops", "metro", 18, 3.1),
            new("Walk 8 min", "Arrive at destination", "walk", 8, 0.5)
        };

        var response = new RoutePlanResponseDto(
            Status: "Planned",
            TotalMinutes: 31,
            TotalDistanceKm: 4.2,
            Steps: steps
        );

        return Task.FromResult(response);
    }
}
