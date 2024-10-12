using Lockbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Application.Contracts;

public interface IApplicationDbContext
{
    public DbSet<PublicKey> PublicKeys { get; set; }
    public DbSet<Domain.Entities.File> Files { get; set; }
    public DbSet<Domain.Entities.FileAccess> FilesAccess { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}