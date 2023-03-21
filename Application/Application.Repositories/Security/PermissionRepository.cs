using Application.Interfaces.Database;
using Application.Repository.Interfaces;

namespace Application.Repositories.Security;

public partial class PermissionRepository: IPermissionRepository
{
    protected IPermissionDatabase db;

    public PermissionRepository(IPermissionDatabase db)
    {
        this.db = db;
    }
}
