namespace Stays.Api.Services.Health;

public sealed class DatabaseHealthProbeService : IHealthProbeService
{
    private readonly StaysDbContext _dbContext;

    public DatabaseHealthProbeService(StaysDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> CanConnectAsync()
    {
        return _dbContext.Database.CanConnectAsync();
    }
}
