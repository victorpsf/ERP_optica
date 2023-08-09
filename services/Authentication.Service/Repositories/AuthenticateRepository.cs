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
        => this.db.Find<AccountDtos.UserDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.FindUserSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@LOGIN", rule.Login, ParameterDirection.Input)
                .Add("@ENTERPRISEID", rule.EnterpriseId, ParameterDirection.Input)
        });

    public AccountDtos.CodeDto? Find(AuthenticateRules.CodeRule rule)
        => this.db.Find<AccountDtos.CodeDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.FindCodeSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });

    public AccountDtos.CodeDto Create(AuthenticateRules.CodeRule rule)
    {
        var codeId = this.db.Execute<int>(new BancoExecuteArgument
        {
            Sql = AuthenticateQueries.CreateCodeSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input),
            Output = "@CODEID"
        });

        var code = this.db.Find<AccountDtos.CodeDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.FindCodeWithKeySql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@CODEID", codeId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });

        if (code is null)
            throw new Exception();

        return code;
    }

    public void Delete(AuthenticateRules.CodeRule rule)
        => this.db.Execute(new BancoArgument
        {
            Sql = AuthenticateQueries.DeleteCodeSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });
}
