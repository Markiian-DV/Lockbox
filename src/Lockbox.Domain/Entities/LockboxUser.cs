using Lockbox.Domain.Common;

namespace Lockbox.Domain.Entities;

public class LockboxUser : BaseEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}