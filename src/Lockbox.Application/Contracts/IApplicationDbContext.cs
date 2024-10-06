using Lockbox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Application.Contracts;

public interface IApplicationDbContext
{
    public DbSet<PublicKey> PublicKeys { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}