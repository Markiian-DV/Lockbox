using System.Net;
using System.Net.Mail;
using Lockbox.Application.Contracts;
using Lockbox.Application.Models;

namespace Lockbox.Infrastructure.EmailServices;

internal class LocalEmailSender : IMailService
{
    public async Task<Result> SendEmailAsync(Email email)
    {
        // TODO: add attachments support
        var mailMessage = new MailMessage
        {
            From = new MailAddress("lockbox@localhost.com", "lockbox"),
            Subject = email.Subject,
            Body = email.Content,
            IsBodyHtml = true
        };

        foreach (var addressee in email.To)
        {
            mailMessage.To.Add(addressee);
        }

        using var smtpClient = new SmtpClient("localhost", 25)
        {
            Credentials = new NetworkCredential("", ""),
            EnableSsl = false
        };

        await smtpClient.SendMailAsync(mailMessage);

        return new Result();
    }
}
