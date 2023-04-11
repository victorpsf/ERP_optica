namespace Application.Interfaces;

public interface IAppControllerServices
{
    public IJwtService jwt { get; }
    public IAppLogger logger { get; }
    public ILoggedUser loggedUser { get; }
    public IMessage message { get; }
    public IUserLanguage userLanguage { get; }
    public ISmtpService smtpService { get; }
}
