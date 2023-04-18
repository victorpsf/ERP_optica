using Application.Interfaces;
using Application.Interfaces.Security;

namespace Application.Services;

public class AppControllerServices: IAppControllerServices
{
    public IJwtService jwt { get; }
    public IAppLogger logger { get; }
    public ILoggedUser loggedUser { get; }
    public IMessage message { get; }
    public IUserLanguage userLanguage { get; }
    public ISmtpService smtpService { get; }
    public IModelValidation validation { get; }
    public IAppSecurity security { get; }

    public AppControllerServices(
        IJwtService jwt, 
        IAppLogger logger, 
        ILoggedUser loggedUser, 
        IUserLanguage userLanguage, 
        ISmtpService smtpService, 
        IModelValidation validation,
        IAppSecurity security
    ) {
        this.jwt = jwt;
        this.logger = logger;
        this.loggedUser = loggedUser;
        this.userLanguage = userLanguage;
        this.message = this.userLanguage.message;
        this.smtpService = smtpService;
        this.validation = validation;
        this.security = security;
    }
}
