using Microsoft.EntityFrameworkCore;
using Stays.Domain.Models;

namespace Stays.Domain.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Content)
            .IsRequired();

        builder.Property(n => n.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(n => n.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(n => n.AuthorUser)
            .WithMany(u => u.Notes)
            .HasForeignKey(n => n.AuthorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(n => n.Visit)
            .WithMany(v => v.Notes)
            .HasForeignKey(n => n.VisitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(n => n.AuthorUserId)
            .HasDatabaseName("IX_Notes_AuthorUserId");

        builder.HasIndex(n => n.VisitId)
            .HasDatabaseName("IX_Notes_VisitId");
    }
}