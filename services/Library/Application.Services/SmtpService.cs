using Application.Interfaces.Services;
using Application.Interfaces.Utils;
using static Application.Base.Models.SmtpServiceModels;

namespace Application.Services;

public class SmtpService: ISmtpService
{
    protected readonly IAppConfigurationManager appConfigurationManager;
    protected readonly IAppLogger logger;

    public SmtpService(IAppConfigurationManager appConfigurationManager, IAppLogger logger)
    {
        this.appConfigurationManager = appConfigurationManager;
        this.logger = logger;
    }

    private String getEnv(string Key) => this.appConfigurationManager.GetProperty("Service", "Smtp", Key);

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
