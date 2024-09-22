using Lockbox.Application.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lockbox.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserCommand registerUser)
    {
        var res = await _sender.Send(registerUser);
        return Ok();
    }
}