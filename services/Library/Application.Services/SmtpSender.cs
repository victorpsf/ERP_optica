using static Application.Base.Models.SmtpServiceModels;
using System.Net.Mail;
using System.Net;

namespace Application.Services;

public class SmtpSender
{
    protected SmtpConfiguration configuration;
    protected SmtpSenderModel model;

    private SmtpSender(SmtpConfiguration configuration, SmtpSenderModel model)
    {
        this.configuration = configuration;
        this.model = model;
    }

    public static SmtpSender GetInstance(SmtpConfiguration configuration, SmtpSenderModel model) => new SmtpSender(configuration, model);

    private NetworkCredential GetCredential() => new NetworkCredential
    {
        UserName = this.configuration.User,
        Password = this.configuration.Password
    };

    private SmtpClient GetClient() => new SmtpClient
    {
        Host = this.configuration.Host,
        Port = this.configuration.Port,
        Credentials = this.GetCredential(),
        EnableSsl = this.configuration.Ssl
    };

    private MailAddress GetMailAddress(string address) => new MailAddress(address);

    private MailMessage GetMessage()
    {
        MailMessage message = new MailMessage
        {
            From = this.GetMailAddress(this.configuration.User)
        };

        foreach (string to in this.model.To)
            message.To.Add(this.GetMailAddress(to));

        return message;
    }

    public void Send()
    {
        var client = this.GetClient();
        var message = this.GetMessage();

        message.Subject = this.model.Subject;
        message.Body = this.model.Message;
        message.IsBodyHtml = this.model.isHtml;

        client.Send(message);
    }
}
