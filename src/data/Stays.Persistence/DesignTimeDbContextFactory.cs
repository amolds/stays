
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Stays.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StaysDbContext>
{
    public StaysDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<DesignTimeDbContextFactory>();
        var configuration = builder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<StaysDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("StaysDb"));

        return new StaysDbContext(optionsBuilder.Options);
    }
}