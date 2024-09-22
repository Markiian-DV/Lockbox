using Lockbox.Application.Models;
using Lockbox.Application.Contracts;
using Lockbox.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Lockbox.Infrastructure.Identity;

internal class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> RegisterUserAsync(string userName, string email, string password)
    {
        var user = new ApplicationUser
        {
            Email = email,
            UserName = userName,
            // TODO: change to false and send confirmation email
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            // TODO: create roles constants at domain layer
            await _userManager.AddToRoleAsync(user, Roles.User);
            return new Result();
        }

        return new Result
        {
            Success = false,
            Error = string.Join("\n", result.Errors.Select(err => err.Description))
        };
    }
}