using System.ComponentModel.DataAnnotations;

namespace EngineWays.Backend.Infrastructure;

public class GtfsStop
{
    public string StopId { get; set; } = default!;
    public string? StopCode { get; set; }
    public string StopName { get; set; } = default!;
    public string? StopDesc { get; set; }
    public double StopLat { get; set; }
    public double StopLon { get; set; }
    public string? ZoneId { get; set; }
    public string? StopUrl { get; set; }
    public int? LocationType { get; set; }
    public string? ParentStation { get; set; }
}

public class GtfsRoute
{
    public string RouteId { get; set; } = default!;
    public string? AgencyId { get; set; }
    public string RouteShortName { get; set; } = default!;
    public string RouteLongName { get; set; } = default!;
    public string? RouteDesc { get; set; }
    public int RouteType { get; set; }
    public string? RouteUrl { get; set; }
    public string? RouteColor { get; set; }
    public string? RouteTextColor { get; set; }
}

public class GtfsTrip
{
    public string TripId { get; set; } = default!;
    public string RouteId { get; set; } = default!;
    public string ServiceId { get; set; } = default!;
    public string? TripHeadsign { get; set; }
    public string? TripShortName { get; set; }
    public int? DirectionId { get; set; }
    public string? BlockId { get; set; }
    public string? ShapeId { get; set; }
    public int? WheelchairAccessible { get; set; }
    public int? BikesAllowed { get; set; }
}
