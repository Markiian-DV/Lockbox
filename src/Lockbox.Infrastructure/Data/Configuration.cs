using Lockbox.Domain.Entities;
using Lockbox.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Lockbox.Domain.Entities.File;
using FileAccess = Lockbox.Domain.Entities.FileAccess;

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

public class FileAccessKeyConfigs : IEntityTypeConfiguration<FileAccess>
{
    public void Configure(EntityTypeBuilder<FileAccess> builder)
    {
        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder
            .HasOne<File>()
            .WithMany()
            .HasForeignKey(e => e.FileId)
            .IsRequired();
    }   
}