using Application.Dtos;
using Application.Interfaces.Connections;
using Authentication.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;
using System.Data;
using Authentication.Service.Repositories.Queries;

namespace Authentication.Service.Repositories;

public class AuthenticateRepository
{
    private IAuthenticateDatabase db;

    public AuthenticateRepository(IAuthenticateDatabase db)
    {
        this.db = db;
    }

    public AccountDtos.UserDto? Find(AuthenticateRules.SingInRule rule)
    {
        return this.db.Find<AccountDtos.UserDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.FindUserSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@LOGIN", rule.Login, ParameterDirection.Input)
                .Add("@ENTERPRISEID", rule.EnterpriseId, ParameterDirection.Input)
        });
    }
}
