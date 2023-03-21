using Application.Interfaces.Database;
using Application.Repository.Interfaces;

namespace Application.Repositories.Security;

public partial class AuthorizationRepository: IAuthorizationRepository
{
    protected IAuthorizationDatabase db;

    public AuthorizationRepository(IAuthorizationDatabase db)
    {
        this.db = db;
    }
}
