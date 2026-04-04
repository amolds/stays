
using Microsoft.EntityFrameworkCore;
using Stays.Domain.Models;

public class StaysDbContext : DbContext
{
    public StaysDbContext(DbContextOptions<StaysDbContext> options) : base(options)
    {
    }

    public DbSet<EmailVerification> EmailVerifications { get; set; }
    public DbSet<Family> Families { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCredential> UserCredentials { get; set; }
    public DbSet<Visit> Visits { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StaysDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}