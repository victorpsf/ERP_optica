using Application.Interfaces.Middleware;
using Application.Interfaces.Security;
using static Application.Base.Models.SecurityModels;
namespace Application.Interfaces.Services;

public interface IBaseControllerServices
{
    public IHash NewHash(AppHashAlgorithm cipher);
    public IPbkdf2Security NewPbkdf2();

    public ILoggedUser loggedUser { get; }
    public IJwtService jwtService { get; }
    public IAppLogger logger { get; }
    public ISmtpService smtpService { get; }
    public IAttributeValidationBase validator { get; }
    public IHostCache hostCache { get; }

    public string getMessage(Base.Models.MultiLanguageModels.MessagesEnum? stack);
}
