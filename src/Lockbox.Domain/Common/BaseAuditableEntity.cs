namespace Lockbox.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity<int>
{
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
}