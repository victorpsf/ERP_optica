using Application.Interfaces.Security;

namespace Application.Services;

public class AppSecurity: IAppSecurity
{
    public IHash hash { get; }
    public IPbkdf2Security pdkf2 { get; }

    public AppSecurity(IHash hash, IPbkdf2Security pdkf2)
    {
        this.hash = hash;
        this.pdkf2 = pdkf2;
    }
}
