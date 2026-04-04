using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stays.Domain.Models;

namespace Stays.Domain.Configuration;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(p => p.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(p => p.OwnerUser)
            .WithMany(u => u.Photos)
            .HasForeignKey(p => p.OwnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Visit)
            .WithMany(v => v.Photos)
            .HasForeignKey(p => p.VisitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.OwnerUserId)
            .HasDatabaseName("IX_Photos_OwnerUserId");

        builder.HasIndex(p => p.VisitId)
            .HasDatabaseName("IX_Photos_VisitId");
    }
}
