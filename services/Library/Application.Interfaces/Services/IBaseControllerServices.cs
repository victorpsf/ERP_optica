using Application.Interfaces.Middleware;
using Application.Interfaces.Security;
using Application.Interfaces.Utils;
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
    public IPrimitiveConverter primitiveConverter { get; }

    public string getMessage(Base.Models.MultiLanguageModels.MessagesEnum? stack);
    public T decodeQueryString<T>(string queryString) where T : class, new();
}
