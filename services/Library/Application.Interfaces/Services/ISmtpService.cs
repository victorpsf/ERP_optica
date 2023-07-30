using static Application.Base.Models.SmtpServiceModels;

namespace Application.Interfaces.Services;

public interface ISmtpService
{
    public bool Send(SmtpSenderModel model);
}
