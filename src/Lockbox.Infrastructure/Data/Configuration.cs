using Lockbox.Domain.Entities;
using Lockbox.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lockbox.Infrastructure.Data;

public class PublicKeyConfigs : IEntityTypeConfiguration<PublicKey>
{
    public void Configure(EntityTypeBuilder<PublicKey> builder)
    {
        builder
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<PublicKey>(e => e.UserId)
            .IsRequired();
    }   
}
