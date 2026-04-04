using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stays.Api.Models;
using Stays.Api.Services.Health;

namespace Stays.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IHealthProbeService _healthProbeService;

    public HealthController(IHealthProbeService healthProbeService)
    {
        _healthProbeService = healthProbeService;
    }

    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok(new HealthResponse("Healthy", DateTime.UtcNow));
    }

    [HttpGet("ready")]
    public async Task<IActionResult> GetReadiness()
    {
        try
        {
            if (await _healthProbeService.CanConnectAsync())
            {
                return Ok(new HealthResponse("Ready", DateTime.UtcNow));
            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                new HealthResponse("Database unavailable", DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                new HealthResponse($"Readiness check failed: {ex.Message}", DateTime.UtcNow));
        }
    }

    [HttpGet("live")]
    public IActionResult GetLiveness()
    {
        return Ok(new HealthResponse("Alive", DateTime.UtcNow));
    }
}
