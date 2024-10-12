using Lockbox.Domain.Common;
using Lockbox.Domain.Enums;

namespace Lockbox.Domain.Entities;

public class FileAccess : BaseAuditableEntity
{
    public int Id { get; set; }
    public Guid FileId { get; set; }
    public string UserId { get; set; }
    public byte[] EncryptedFileAccessKey { get; set; }
    public AccessLevel AccessLevel { get; set; }
    public  DateTimeOffset? RevokedData { get; set; }
}