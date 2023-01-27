using Dapper;
using System.Data;
using static Application.Library.AuthenticationModels;
using static Application.Library.DatabaseModels;
using static Application.Library.JwtModels;
using static Authentication.Repositories.Rules.AuthenticationRules;

namespace Authentication.Repositories;

public partial class AuthenticationRepository
{
    public LoggedUserDto? Find(ClaimIdentifier claim)
    {
        this.factory.Connect();
        var parameters = new DynamicParameters();

        parameters.Add(name: "@USERID", value: claim.UserId, direction: ParameterDirection.Input);
        parameters.Add(name: "@ENTERPRISEID", value: claim.EnterpriseId, direction: ParameterDirection.Input);

        LoggedUserDto? result = this.factory.Find<LoggedUserDto>(new BancoArgument
        {
            Sql = FindUserByIdSql,
            Parameter = parameters,
            CmdType = (int)CommandType.Text
        });

        this.factory.Disconnect();

        return result;
    }

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

    //public ForgottenDto Find(FindUserForgottenRule business)
    //{
    //    var parameters = new DynamicParameters();
    //    var querySql = string.Empty;
    //    if (business.FindBy == FindType.Name)
    //    {
    //        parameters.Add(name: "@USERNAME", value: business.Name, direction: ParameterDirection.Input);
    //        querySql = FindUserByNameSql;
    //    }

    //    else
    //    {
    //        parameters.Add(name: "@USEREMAIL", value: business.Email, direction: ParameterDirection.Input);
    //        querySql = FindUserByEmailSql;
    //    }

    //    this.factory.Connect();
    //    var result = this.factory.Find<ForgottenDto>(new BancoArgument
    //    {
    //        Sql = querySql,
    //        Parameter = parameters
    //    });

    //    this.factory.Disconnect();

    //    if (result is null)
    //        throw new Exception("USER_NOT_FOUND");
    //    return result;
    //}

    //public ForgottenCodeDto? Find(FindCodeForgottenRule business)
    //{
    //    var parameters = new DynamicParameters();
    //    parameters.Add(name: "@USERID", value: business.UserId, direction: ParameterDirection.Input);

    //    this.factory.Connect();
    //    ForgottenCodeDto? result = this.factory.Find<ForgottenCodeDto>(new BancoArgument
    //    {
    //        Sql = FindCodeByUserIdSql,
    //        Parameter = parameters
    //    });
    //    this.factory.Disconnect();

    //    return result;
    //}



    //public UserCodeDto? Find(FindUserCodeRule business)
    //{
    //    var parameters = new DynamicParameters();
    //    parameters.Add(name: "@USERID", value: business.UserId, direction: ParameterDirection.Input);

    //    this.factory.Connect();
    //    var code = this.factory.Find<UserCodeDto>(new BancoArgument
    //    {
    //        Sql = FindUserCodeSql,
    //        Parameter = parameters
    //    });
    //    this.factory.Disconnect();

    //    return code;
    //}
}
