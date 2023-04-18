using Application.Interfaces.Database;
using Application.Repository.Interfaces;

namespace Application.Repositories.Security;

public partial class PermissionRepository: IPermissionRepository
{
    protected IAuthorizationDatabase db;

    public PermissionRepository(IAuthorizationDatabase db)
    {
        this.db = db;
    }
}
