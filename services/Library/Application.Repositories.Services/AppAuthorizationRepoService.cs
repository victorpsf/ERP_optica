using Application.Base.Models;
using Application.Exceptions;
using Application.Interfaces.Connections;
using Application.Interfaces.RepoServices;
using Application.Interfaces.Repositories;
using Application.Services;
using static Application.Business.Rules.AppAuthorizationRepoServiceModels;

namespace Application.Repositories.Services;

public class AppAuthorizationRepoService: BaseRepoService<IAuthorizationDatabase>, IAppAuthorizationRepoService
{
    public IAppAuthorizationRepository AppAuthorizationRepository { get; }

    public AppAuthorizationRepoService(IAuthorizationDatabase db): base(db)
    {
        this.AppAuthorizationRepository = new AppAuthorizationRepository(db);
    }

    public bool VerifyPermission(VerifyPermissionRule rule) => this.ExecuteQuery(
            this.AppAuthorizationRepository.CountPermission,
            rule,
            false
        ) > 0;
}
