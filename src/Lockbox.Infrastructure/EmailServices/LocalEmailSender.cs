using System.Net.Mail;
using Lockbox.Application.Contracts;
using Lockbox.Application.Models;
using Microsoft.Extensions.Options;

namespace Lockbox.Infrastructure.EmailServices;

internal class LocalEmailSender : IMailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public LocalEmailSender(IOptions<EmailConfiguration> options)
    {
        _emailConfiguration = options.Value;
    }

    public async Task<Result> SendEmailAsync(Email email)
    {
        // TODO: add attachments support
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailConfiguration.SenderAddress),
            Subject = email.Subject,
            Body = email.Content,
            IsBodyHtml = true
        };

        foreach (var addressee in email.To)
        {
            mailMessage.To.Add(addressee);
        }

        using var smtpClient = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch
        {
            // local sender, do nothing
        }

        return new Result();
    }
}
