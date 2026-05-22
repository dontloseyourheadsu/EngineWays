using EngineWays.Backend.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EngineWays.Backend.Features.Stops;

[ApiController]
[Route("api/stops")]
public class StopsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public StopsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StopSearchResultDto>>> Search([FromQuery] string? query, [FromQuery] int limit = 20, CancellationToken cancellationToken = default)
    {
        var take = Math.Clamp(limit, 1, 50);
        var stopsQuery = _dbContext.Stops.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var pattern = $"%{query.Trim()}%";
            stopsQuery = stopsQuery.Where(stop =>
                EF.Functions.ILike(stop.StopName, pattern) ||
                (stop.StopCode != null && EF.Functions.ILike(stop.StopCode, pattern)));
        }

        var results = await stopsQuery
            .OrderBy(stop => stop.StopName)
            .Take(take)
            .Select(stop => new StopSearchResultDto(
                stop.StopId,
                stop.StopName,
                stop.StopLat,
                stop.StopLon,
                stop.StopCode
            ))
            .ToListAsync(cancellationToken);

        return Ok(results);
    }
}
