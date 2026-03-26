
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var services = new ServiceCollection();
var connectionString = configuration.GetConnectionString("StaysDb");

services.AddDbContext<StaysDbContext>(options =>
    options.UseSqlServer(connectionString));

var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StaysDbContext>();
    await dbContext.Database.MigrateAsync();

    Console.WriteLine("Migration completed successfully.");
}

// TODO: seed db
// context.Users.Add(new User { ... });
// await context.SaveChangesAsync();