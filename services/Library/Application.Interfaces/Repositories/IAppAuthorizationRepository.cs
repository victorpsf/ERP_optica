using static Application.Business.Rules.AppAuthorizationRepoServiceModels;

namespace Application.Interfaces.Repositories;

public interface IAppAuthorizationRepository
{
    int CountPermission(VerifyPermissionRule rule);
}
