using Application.Interfaces.Connections;
using Application.Interfaces.Repositories;
using Application.Repositories.Queries;
using static Application.Base.Models.JwtModels;
using static Application.Business.Rules.AppAuthenticationRepoServiceModels;
using static Application.Base.Models.DatabaseModels;
using System.Data;

namespace Application.Repositories;

public class AppAuthenticationRepository: IAppAuthenticationRepository
{
    private IAuthenticationDatabase db;

    public AppAuthenticationRepository(IAuthenticationDatabase db)
    { this.db = db; }

    public LoggedUserDto? Find(FindClaimUserRule rule)
    {
        return this.db.Find<LoggedUserDto>(new BancoArgument
        {
            Sql = AuthenticationQueries.VerifyAuthenticationUser,
            Parameter = ParameterCollection.GetInstance()
                .Add("@USEID", rule.claim.UserIdInt(), ParameterDirection.Input)
                .Add("@ENTERPRISEID", rule.claim.EnterpriseIdInt(), ParameterDirection.Input)
                .Add("@TOKEN", rule.claim.Token, ParameterDirection.Input)
        });
    }
}
