using Lockbox.Domain.Common;

namespace Lockbox.Domain.Entities;

/// <summary>
/// Represents encrypted 128 bit symmetric key used to encrypt and decrypt a file.
/// </summary>
public class FileAccessKey : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public int FileId { get; set; }
    public byte[] EncryptedKey { get; set; }
}