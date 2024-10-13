using Lockbox.Application.Files.Commands;
using Lockbox.Application.Files.Queries;
using Lockbox.Web.Infrastructure;
using Lockbox.Web.WebModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lockbox.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FilesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly UserContext _userContext;

    public FilesController(ISender sender, UserContext userContext)
    {
        _sender = sender;
        _userContext = userContext;
    }

    [HttpPost("upload")]
    [RequestSizeLimit(1024 * 1024 * 10)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        await _sender.Send(new CreateFileCommand(file.FileName, stream, file.ContentType, _userContext.UserId));
        return Ok();
    }

    [HttpPost("download")]
    public async Task<IActionResult> DownloadFile(GetFileRequest request)
    {
        var response = await _sender.Send(new GetFileCommand(_userContext.UserId, request.FileId, request.Key));
        // should not dispose stream, it will be disposed auto-magically 
        return new FileStreamResult(response.Stream, "application/octet-stream")
        {
            FileDownloadName = response.FileName
        };
    }

    [HttpGet("info")]
    public async Task GetFilesInfo()
    {

    }
}