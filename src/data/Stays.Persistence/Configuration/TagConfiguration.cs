using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stays.Domain.Models;

namespace Stays.Domain.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(t => t.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(t => t.OwnerUser)
            .WithMany(u => u.Tags)
            .HasForeignKey(t => t.OwnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(t => t.OwnerUserId)
            .HasDatabaseName("IX_Tags_OwnerUserId");
    }
}