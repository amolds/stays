namespace Stays.Api.Services.Health;

public interface IHealthProbeService
{
    Task<bool> CanConnectAsync();
}
