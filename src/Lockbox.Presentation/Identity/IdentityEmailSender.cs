using Lockbox.Application.Contracts;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace Lockbox.Presentation.Identity;

// this is basically proxy for IMailService since identity`s IEmailSender cant be accessed from classlib 
public class IdentityEmailSender : IEmailSender
{
    private readonly IMailService _mailService;

    public IdentityEmailSender(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await _mailService.SendEmailAsync(new Application.Models.Email
        {
            To = [email],
            Subject = subject,
            Content = htmlMessage
        });
    }
}