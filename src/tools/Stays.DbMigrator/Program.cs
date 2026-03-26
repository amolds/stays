
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stays.Domain.Models;

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
    await Seed.Reset(dbContext);

    Console.WriteLine("Migration completed successfully.");

    await Seed.SeedData(dbContext);
    Console.WriteLine("Data seeded successfully.");

    var lonely = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "jon.lonely@outlook.com");
    Console.WriteLine($"User: {lonely?.DisplayName}, Family: {lonely?.Family?.Name}");
    
    var family = await dbContext.Families
        .Include(f => f.FamilyMembers)
        .FirstOrDefaultAsync();    
    Console.WriteLine($"Family: {family?.Name}, Members: {string.Join(", ", family?.FamilyMembers.Select(m => m.DisplayName) ?? Array.Empty<string>())}");

}
