namespace Lockbox.Application.Models;

public class Email
{
        public List<string> To { get; set; } = default!;
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        // how to pass files?
}