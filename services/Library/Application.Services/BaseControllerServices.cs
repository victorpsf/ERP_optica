using Application.Interfaces.Messages;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using Application.Security;
using static Application.Base.Models.SecurityModels;

namespace Application.Services;

public class BaseControllerServices: IBaseControllerServices
{
    public IHash NewHash(AppHashAlgorithm cipher) => Hash.Create(cipher);
    public IPbkdf2Security NewPbkdf2() => new Pbkdf2Security();

    public ILoggedUser loggedUser { get; }
    public IJwtService jwtService { get; }
    public IAppLogger logger { get; }
    public ISmtpService smtpService { get; }
    public IAttributeValidationBase validator { get; }

    public string getMessage(Base.Models.MultiLanguageModels.MessagesEnum? stack)
        => this.loggedUser.message.GetMessage(stack);

    public BaseControllerServices(
        ILoggedUser loggedUser,
        IJwtService jwtService,
        IAppLogger logger,
        ISmtpService smtpService,
        IAttributeValidationBase validator
    )
    {
        this.logger = logger;
        this.loggedUser = loggedUser;
        this.jwtService = jwtService;
        this.smtpService = smtpService;
        this.validator = validator;
    }
}
