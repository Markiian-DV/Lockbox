namespace Lockbox.Web.WebModels;

public class GetFileRequest
{
    public Guid FileId { get; set; }
    public string Key { get; set; }
}