using Application.Base.Models;
using Application.Exceptions;
using Application.Interfaces.Connections;
using Application.Interfaces.RepoServices;
using Application.Interfaces.Repositories;
using Application.Services;
using static Application.Base.Models.JwtModels;
using static Application.Business.Rules.AppAuthenticationRepoServiceModels;

namespace Application.Repositories.Services;

public class AppAuthenticationRepoService: BaseRepoService<IAuthenticationDatabase>, IAppAuthenticationRepoService
{
    public IAppAuthenticationRepository AppAuthenticationRepository { get; }

    public AppAuthenticationRepoService(IAuthenticationDatabase db): base(db)
    {
        this.AppAuthenticationRepository = new AppAuthenticationRepository(db);
    }

    public LoggedUserDto Find(FindClaimUserRule rule)
    {
        LoggedUserDto? loggedUser = this.ExecuteQuery(
            this.AppAuthenticationRepository.Find,
            rule,
            false
        );

        if (loggedUser is null)
            throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_DONT_FIND_CLAIM_USER);

        return loggedUser;
    }
}
