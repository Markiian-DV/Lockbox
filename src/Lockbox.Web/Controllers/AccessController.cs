using Lockbox.Application.Access.Commands;
using Lockbox.Web.Infrastructure;
using Lockbox.Web.WebModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lockbox.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccessController : ControllerBase
{
    private readonly ISender _sender;
    private readonly UserContext _userContext;

    public AccessController(ISender sender, UserContext userContext)
    {
        _sender = sender;
        _userContext = userContext;
    }

    [HttpPost("grant")]
    public async Task<IActionResult> Grant(GrantAccessRequest request)
    {
        await _sender.Send(new GrantAccessCommand(
            _userContext.UserId,
            request.TargetUserEmail,
            request.FileId,
            request.AccessLevel,
            request.PrivateKey));

        return Ok();
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke(RevokeAccessRequest request)
    {
        await _sender.Send(new RevokeAccessCommand(_userContext.UserId, request.TargetUserEmail, request.FileId));
        return Ok();
    }
}