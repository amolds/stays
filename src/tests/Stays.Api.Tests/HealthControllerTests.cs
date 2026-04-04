using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stays.Api.Controllers;
using Stays.Api.Models;
using Stays.Api.Services.Health;

namespace Stays.Api.Tests;

public class HealthControllerTests
{
    [Fact]
    public async Task GetReadiness_ReturnsOk_WhenDatabaseIsHealthy()
    {
        var controller = new HealthController(new FakeHealthProbeService(true));

        var result = await controller.GetReadiness();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<HealthResponse>(okResult.Value);

        Assert.Equal("False", response.Status);
    }

    [Fact]
    public async Task GetReadiness_ReturnsServiceUnavailable_WhenDatabaseIsUnavailable()
    {
        var controller = new HealthController(new FakeHealthProbeService(false));

        var result = await controller.GetReadiness();

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status503ServiceUnavailable, statusResult.StatusCode);

        var response = Assert.IsType<HealthResponse>(statusResult.Value);
        Assert.Equal("Database unavailable", response.Status);
    }


    [Fact]
    public async Task GetReadiness_ReturnsServiceUnavailable_WhenProbeThrows()
    {
        var controller = new HealthController(new ThrowingHealthProbeService());

        var result = await controller.GetReadiness();

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status503ServiceUnavailable, statusResult.StatusCode);

        var response = Assert.IsType<HealthResponse>(statusResult.Value);
        Assert.StartsWith("Readiness check failed:", response.Status);
    }

    private sealed class FakeHealthProbeService : IHealthProbeService
    {
        private readonly bool _canConnect;

        public FakeHealthProbeService(bool canConnect)
        {
            _canConnect = canConnect;
        }

        public Task<bool> CanConnectAsync() => Task.FromResult(_canConnect);
    }

    private sealed class ThrowingHealthProbeService : IHealthProbeService
    {
        public Task<bool> CanConnectAsync() => throw new InvalidOperationException("Probe failure");
    }
}
