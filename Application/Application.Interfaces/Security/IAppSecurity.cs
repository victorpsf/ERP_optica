namespace Application.Interfaces.Security;

public interface IAppSecurity
{
    IHash hash { get; }
    IPbkdf2Security pdkf2 { get; }
}
