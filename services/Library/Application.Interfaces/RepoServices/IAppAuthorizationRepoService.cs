using Application.Interfaces.Repositories;
using static Application.Business.Rules.AppAuthorizationRepoServiceModels;

namespace Application.Interfaces.RepoServices;

public interface IAppAuthorizationRepoService
{
    IAppAuthorizationRepository AppAuthorizationRepository { get; }

    bool VerifyPermission(VerifyPermissionRule rule);
}
