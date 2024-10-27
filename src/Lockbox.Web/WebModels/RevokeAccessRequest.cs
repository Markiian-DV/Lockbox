namespace Lockbox.Web.WebModels;

public class RevokeAccessRequest
{
    public string FileId { get; set; }
    public string TargetUserEmail { get; set; }
}