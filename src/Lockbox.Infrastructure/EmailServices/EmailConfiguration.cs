namespace Lockbox.Infrastructure.EmailServices;

internal class EmailConfiguration
{
    public int SmtpPort { get; set; }
    public string SmtpServer { get; set; } = string.Empty;
    public string SenderAddress { get; set; } = string.Empty;
}