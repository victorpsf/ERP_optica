using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using static Application.Models.Services.SmtpServiceModels;

namespace Application.Services;

public class SmtpService: ISmtpService
{
    protected readonly IConfiguration configuration;
    protected readonly IAppLogger logger;

    public SmtpService (IConfiguration configuration, IAppLogger logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    private String getEnv(string Key)
    {
#if DEBUG
        return this.configuration.GetSection($"Smtp:{Key}").Value ?? string.Empty;
#elif RELEASE
        return this.configuration.GetSection($"Smtp{Key}").Value ?? string.Empty;
#endif
    }

    private SmtpConfiguration getConfig() => new SmtpConfiguration
    {
        User = this.getEnv("User"),
        Password = this.getEnv("Password"),
        Host = this.getEnv("Host"),
        Port = Convert.ToInt32(this.getEnv("Port")),
        Ssl = true
    };

    public bool Send(SmtpSenderModel model)
    {
        try
        {
            SmtpSender.GetInstance(this.getConfig(), model)
                .Send();
            return true;
        }

        catch (Exception ex)
        {
            logger.Error("FAILURE IN SEND SMTP", ex);
            return false;
        }
    }
}
