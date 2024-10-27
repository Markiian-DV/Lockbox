using Lockbox.Domain.Enums;

namespace Lockbox.Web.WebModels;

public class GrantAccessRequest
{
    public string FileId { get; set; }
    public string TargetUserEmail { get; set; }
    public AccessLevel AccessLevel { get; set; }
    public string PrivateKey { get; set; }
}