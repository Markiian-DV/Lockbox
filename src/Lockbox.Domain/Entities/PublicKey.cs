using Lockbox.Domain.Common;

namespace Lockbox.Domain.Entities;

public class PublicKey : BaseAuditableEntity
{
    public required string KeyValue { get; set; }
    public required string UserId { get; set; }
}
