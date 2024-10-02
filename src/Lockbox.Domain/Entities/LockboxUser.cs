using Lockbox.Domain.Common;

namespace Lockbox.Domain.Entities;

public class LockboxUser : BaseEntity<Guid>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    // public key?
}