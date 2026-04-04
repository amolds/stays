using Microsoft.EntityFrameworkCore;
using Stays.Domain.Models;

namespace Stays.Domain.Configuration;

public class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Family> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(f => f.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(f => f.Name)
            .HasDatabaseName("IX_Families_Name");

        builder.HasMany(f => f.FamilyMembers)
            .WithOne(u => u.Family)
            .HasForeignKey(u => u.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(f => f.CreatedByUser)
            .WithMany()
            .HasForeignKey(f => f.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
