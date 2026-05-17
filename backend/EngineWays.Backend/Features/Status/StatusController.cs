using Microsoft.AspNetCore.Mvc;

namespace EngineWays.Backend.Features.Status;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly ILogger<StatusController> _logger;

    public StatusController(ILogger<StatusController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetStatus()
    {
        _logger.LogInformation("Status check requested.");
        return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
    }
}
