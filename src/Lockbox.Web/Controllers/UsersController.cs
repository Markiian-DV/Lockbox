using Lockbox.Application.UserKeys.Commands;
using Lockbox.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lockbox.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly UserContext _userContext;

    public UserController(ISender sender, UserContext userContext)
    {
        _sender = sender;
        _userContext = userContext;
    }

    [HttpPost("keys")]
    public async Task<string> GenerateKeys()
    {
        var result = await _sender.Send(new CreateUserKeysCommand(_userContext.UserId));
        return result.Value;
    }
}
