using Lockbox.Application.Contracts;
using MediatR;

namespace Lockbox.Application.Auth;

public class RegisterUserResult
{
    public bool Success { get; set; }
    public string PublicKey { get; set; } = string.Empty;
}

public record RegisterUserCommand(string Email, string Password, string UserName) : IRequest<RegisterUserResult>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IAuthService _authService;
    private readonly IMailService _mailService;

    public RegisterUserCommandHandler(IAuthService authService, IMailService mailService)
    {
        _authService = authService;
        _mailService = mailService;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        await _mailService.SendEmailAsync(new Models.Email()
        {
            To = [request.Email],
            Subject = "test subject",
            Content = "test content"

        });
        // create user
        var registerResult = await _authService.RegisterUserAsync(request.UserName, request.Email, request.Password);
        if (registerResult.Failure)
        {
            // use ResultWrapper?
            return new RegisterUserResult { Success = false };
        }

        // create keys, return private

        return new RegisterUserResult
        {
            Success = true,
            PublicKey = "Test"
        };
    }
}