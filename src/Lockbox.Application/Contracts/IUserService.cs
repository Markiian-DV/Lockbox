using Lockbox.Domain.Entities;

namespace Lockbox.Application.Contracts;

public interface IUserService
{
    Task<LockboxUser> GetUser(string userId);
    Task<List<LockboxUser>> GetUsers(IEnumerable<string> userIds);
}