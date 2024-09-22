using Lockbox.Application.Models;

namespace Lockbox.Application.Contracts;

public interface IAuthService
{
    Task<Result> RegisterUserAsync(string userName, string email, string password);
}