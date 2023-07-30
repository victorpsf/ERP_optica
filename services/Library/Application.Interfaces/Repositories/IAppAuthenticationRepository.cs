using static Application.Base.Models.JwtModels;
using static Application.Business.Rules.AppAuthenticationRepoServiceModels;

namespace Application.Interfaces.Repositories;

public interface IAppAuthenticationRepository
{
    LoggedUserDto? Find(FindClaimUserRule rule);
}
