using Application.Interfaces.Connections;
using Application.Interfaces.Repositories;
using static Application.Business.Rules.AppAuthorizationRepoServiceModels;
using static Application.Base.Models.DatabaseModels;
using Application.Repositories.Queries;
using System.Data;

namespace Application.Repositories;

public class AppAuthorizationRepository: IAppAuthorizationRepository
{
    private IAuthorizationDatabase db;

    public AppAuthorizationRepository(IAuthorizationDatabase db)
    { this.db = db; }

    public int CountPermission(VerifyPermissionRule rule)
    {
        return this.db.Find<int>(new BancoArgument
        {
            Sql = AuthorizationQueries.VerifyPermissionUser,
            Parameter = ParameterCollection.GetInstance()
                .Add("@USERID", rule.loggedUser.UserId, ParameterDirection.Input)
                .Add("@ENTERPRISEID", rule.loggedUser.EnterpriseId, ParameterDirection.Input)
                .Add("@PERMISSION", rule.Permission, ParameterDirection.Input)
        });
    }
}
