using Application.Interfaces.Repositories;
using static Application.Base.Models.JwtModels;
using static Application.Business.Rules.AppAuthenticationRepoServiceModels;

namespace Application.Interfaces.RepoServices;

public interface IAppAuthenticationRepoService
{
    IAppAuthenticationRepository AppAuthenticationRepository { get; }

    LoggedUserDto Find(FindClaimUserRule rule);
}
