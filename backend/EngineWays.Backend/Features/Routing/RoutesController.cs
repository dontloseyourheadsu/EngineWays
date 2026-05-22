using Microsoft.AspNetCore.Mvc;

namespace EngineWays.Backend.Features.Routing;

[ApiController]
[Route("api/routes")]
public class RoutesController : ControllerBase
{
    private readonly IRoutingEngine _routingEngine;

    public RoutesController(IRoutingEngine routingEngine)
    {
        _routingEngine = routingEngine;
    }

    [HttpPost("plan")]
    public async Task<ActionResult<RoutePlanResponseDto>> Plan([FromBody] RouteRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Origin is null || request.Destination is null)
        {
            return BadRequest("Origin and destination are required.");
        }

        var response = await _routingEngine.PlanAsync(request, cancellationToken);
        return Ok(response);
    }
}
