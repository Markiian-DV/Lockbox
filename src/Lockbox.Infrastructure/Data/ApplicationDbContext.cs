using System.Reflection;
using Lockbox.Application.Contracts;
using Lockbox.Domain.Entities;
using Lockbox.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private static readonly AuditableEntityInterceptor _auditableInterceptor = new();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<PublicKey> PublicKeys { get; set; }
    public DbSet<Domain.Entities.File> Files { get; set; }
    public DbSet<Domain.Entities.FileAccess> FilesAccess { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
    }
}
