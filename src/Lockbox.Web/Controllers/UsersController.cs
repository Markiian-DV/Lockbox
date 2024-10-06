using System.Security.Claims;
using Lockbox.Application.UserKeys.Commands;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        _sender = sender;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("keys")]
    public async Task<string> GenerateKeys()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _sender.Send(new CreateUserKeysCommand(userId));
        return result.Value;
    }
}