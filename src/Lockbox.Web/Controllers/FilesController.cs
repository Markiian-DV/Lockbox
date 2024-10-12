using Lockbox.Application.Files.Commands;
using Lockbox.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [RequestSizeLimit(1024*1024*10)] 
    public async Task<IActionResult> Upload(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        await _sender.Send(new CreateFileCommand(file.FileName, stream, file.ContentType, _userContext.UserId));
        return Ok();
    }

}