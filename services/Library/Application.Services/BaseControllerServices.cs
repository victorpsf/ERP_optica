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

    public BaseControllerServices(
        ILoggedUser loggedUser,
        IJwtService jwtService,
        IAppLogger logger,
        ISmtpService smtpService
    )
    {
        this.logger = logger;
        this.loggedUser = loggedUser;
        this.jwtService = jwtService;
        this.smtpService = smtpService;
    }
}
