using Lockbox.Domain.Common;
using Lockbox.Domain.Enums;

namespace Lockbox.Domain.Entities;

public class File : BaseAuditableEntity
{
    public required string Name { get; set; }
    public FileType FileType { get; set; }
    public long SizeInBytes { get; set; }
    public Guid OwnerId { get; set; }
}