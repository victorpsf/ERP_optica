using static Application.Repositories.Rules.PermissionRules;

namespace Application.Repository.Interfaces;

public interface IPermissionRepository
{
    bool VerifyPermission(VerifyPermissionRule rule);
}
