
using Microsoft.EntityFrameworkCore;
using Stays.Domain.Models;

public class StaysDbContext : DbContext
{
    public StaysDbContext(DbContextOptions<StaysDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Family> Families { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StaysDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}