using Lockbox.Domain.Common;

namespace Lockbox.Domain.Entities;

public class LockboxUser : BaseEntity
{
    public string Id { get; set; }
    public string Email { get; set; }
}