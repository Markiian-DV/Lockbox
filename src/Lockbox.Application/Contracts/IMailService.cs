using Lockbox.Application.Models;

namespace Lockbox.Application.Contracts;

public interface IMailService
{
    Task<Result> SendEmailAsync(Email email);
}