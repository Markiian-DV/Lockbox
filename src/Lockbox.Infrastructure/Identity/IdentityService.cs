using Lockbox.Application.Contracts;
using Lockbox.Domain.Entities;
using Lockbox.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class IdentityService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<LockboxUser> GetUser(string userId)
    {
        var user = (await _userManager.FindByIdAsync(userId)) ?? throw new Exception("user not found");
        return new()
        {
            Id = user.Id,
            Email = user.Email!
        };
    }

    public async Task<List<LockboxUser>> GetUsers(IEnumerable<string> userIds)
    {
        var users = await _userManager.Users.Where(u => userIds.Contains(u.Id)).Select(u => new LockboxUser
        {
            Id = u.Id,
            Email = u.Email!
        }).ToListAsync();

        return users;
    }

    public async Task<LockboxUser> GetUserByEmail(string email)
    {
        var user =  (await _userManager.FindByEmailAsync(email)) ?? throw new Exception("user not found");
        return new LockboxUser{
            Id = user.Id,
            Email = user.Email!
        };
    }
}