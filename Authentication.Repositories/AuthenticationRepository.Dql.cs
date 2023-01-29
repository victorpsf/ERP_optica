using Dapper;
using System.Data;
using static Application.Library.AuthenticationModels;
using static Application.Library.DatabaseModels;
using static Application.Library.JwtModels;
using static Authentication.Repositories.Rules.AuthenticationRules;

namespace Authentication.Repositories;

public partial class AuthenticationRepository
{
    public UserDto Find(FindUserRule business)
    {
        this.factory.Connect();
        var result = this.factory.Find<UserDto>(new BancoArgument { Sql = FindUserSql, Parameter = this.Build(business) });
        this.factory.Disconnect();

        if (result is null) throw new Exception("USER_DONT_FOUND");
        else return result;
    }

    public UserCodeDto? Find(FindUserCodeRule business)
    {
        var parameters = new DynamicParameters();
        parameters.Add(name: "@AUTHID", value: business.Input.UserId, direction: ParameterDirection.Input);

        this.factory.Connect();
        var result = this.factory.Find<UserCodeDto>(new BancoArgument { Sql = FindUserCodeSql, Parameter = parameters });
        this.factory.Disconnect();

        return result;
    }
}
