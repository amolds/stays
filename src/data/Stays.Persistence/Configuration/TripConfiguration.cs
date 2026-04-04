using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stays.Domain.Models;

namespace Stays.Persistence.Configuration;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(t => t.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(t => t.CreatedByUser)
            .WithMany(u => u.CreatedTrips)
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Visits)
            .WithOne(v => v.Trip)
            .HasForeignKey(v => v.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Tags)
            .WithMany(t => t.TripTags)
            .UsingEntity<Dictionary<string, object>>(
                "TripTag",
                x => x.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.NoAction),
                x => x.HasOne<Trip>().WithMany().HasForeignKey("TripId").OnDelete(DeleteBehavior.NoAction),
                x =>
                {
                    x.HasKey("TripId", "TagId");
                    x.HasIndex("TagId").HasDatabaseName("IX_TripTags_TagId");
                }
            );

        builder.HasIndex(t => t.CreatedByUserId)
            .HasDatabaseName("IX_Trips_CreatedByUserId");
    }
}