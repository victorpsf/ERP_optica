using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using static Application.Library.SmtpModels;

namespace Shared.Services
{
    public class SmtpService
    {
        private readonly IConfiguration Configuration;

        private SmtpService(IConfiguration configuration) =>
            this.Configuration = configuration;

        public static SmtpService Create(IConfiguration configuration) => new SmtpService(configuration);
        private MailMessage CreateMailMessage() => new MailMessage { From = new MailAddress(this.Configuration.GetSection("Smtp:User").Value ?? string.Empty) };

        private NetworkCredential CreateCredential() => new NetworkCredential()
        {
            UserName = this.Configuration.GetSection("Smtp:User").Value,
            Password = this.Configuration.GetSection("Smtp:Password").Value
        };

        private SmtpClient CreateClient() => new SmtpClient
        {
            Host = this.Configuration.GetSection("Smtp:Host").Value ?? string.Empty,
            Port = Convert.ToInt32(this.Configuration.GetSection("Smtp:Port").Value),
            Credentials = this.CreateCredential(),
            EnableSsl = true
        };


        public void Send(SendModel model)
        {
            var client = this.CreateClient();
            var message = this.CreateMailMessage();

            foreach (string to in model.To) message.To.Add(new MailAddress(to));
            message.Subject = model.Subject is not null ? model.Subject : string.Empty;
            message.Body = model.Body is not null ? model.Body : string.Empty;
            message.IsBodyHtml = model.IsHtml;

            client.Send(message);
        }
    }
}
