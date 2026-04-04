using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stays.Domain.Models;

namespace Stays.Domain.Configuration;

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(v => v.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(v => v.Rating)
            .HasPrecision(3, 1);

        builder.HasOne(v => v.User)
            .WithMany(u => u.Visits)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Places)
            .WithMany(p => p.Visits)
            .UsingEntity<Dictionary<string, object>>(
                "VisitPlace",
                x => x.HasOne<Place>().WithMany().HasForeignKey("PlaceId").OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne<Visit>().WithMany().HasForeignKey("VisitId").OnDelete(DeleteBehavior.NoAction),
                x =>
                {
                    x.HasKey("VisitId", "PlaceId");
                    x.HasIndex("PlaceId").HasDatabaseName("IX_VisitPlaces_PlaceId");
                }
            );

        builder.HasMany(v => v.Tags)
            .WithMany(t => t.VisitTags)
            .UsingEntity<Dictionary<string, object>>(
                "VisitTag",
                x => x.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.NoAction),
                x => x.HasOne<Visit>().WithMany().HasForeignKey("VisitId").OnDelete(DeleteBehavior.NoAction),
                x =>
                {
                    x.HasKey("VisitId", "TagId");
                    x.HasIndex("TagId").HasDatabaseName("IX_VisitTags_TagId");
                }
            );

        builder.HasIndex(v => v.UserId)
            .HasDatabaseName("IX_Visits_UserId");
    }
}
