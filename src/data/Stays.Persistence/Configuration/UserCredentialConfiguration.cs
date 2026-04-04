using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stays.Domain.Models;

namespace Stays.Domain.Configuration;

public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
{
    public void Configure(EntityTypeBuilder<UserCredential> builder)
    {
        builder.HasKey(uc => uc.UserId);

        builder.HasOne(uc => uc.User)
            .WithOne(u => u.Credential)
            .HasForeignKey<UserCredential>(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}