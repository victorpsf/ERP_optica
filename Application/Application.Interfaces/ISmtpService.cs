using static Application.Models.Services.SmtpServiceModels;

namespace Application.Interfaces;

public interface ISmtpService
{
    public bool Send(SmtpSenderModel model);
}
