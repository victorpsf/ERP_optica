using Application.Interfaces.Security;

namespace Application.Interfaces;

public interface IAppControllerServices
{
    IJwtService jwt { get; }
    IAppLogger logger { get; }
    ILoggedUser loggedUser { get; }
    IMessage message { get; }
    IUserLanguage userLanguage { get; }
    ISmtpService smtpService { get; }
    IModelValidation validation { get; }
    IAppSecurity security { get; }
}
