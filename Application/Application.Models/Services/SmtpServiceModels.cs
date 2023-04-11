namespace Application.Models.Services;

public static class SmtpServiceModels
{
    public class SmtpConfiguration
    {
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }

    public class SmtpSenderModel
    {
        public string[] To { get; set; } = Array.Empty<string>();
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool isHtml { get; set; } = false;
    }
}
