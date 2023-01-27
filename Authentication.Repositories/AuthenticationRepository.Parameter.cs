using Dapper;
using System.Data;
using static Authentication.Repositories.Rules.AuthenticationRules;
using Shared.Extensions;

namespace Authentication.Repositories;

public partial class AuthenticationRepository
{
    private DynamicParameters Build(FindUserRule business) {
        var parameters = new DynamicParameters();
        parameters.Add(name: "@USERNAME", value: business.Input.Name, direction: ParameterDirection.Input);
        parameters.Add(name: "@ENTERPRISEID", value: business.Input.EnterpriseId, direction: ParameterDirection.Input);
        return parameters;
    }

    private DynamicParameters Build(CreateUserCode business) {
        var parameters = new DynamicParameters();
        parameters.Add(name: "@AUTHID", value: business.Input.UserId, direction: ParameterDirection.Input);
        parameters.Add(name: "@CODETYPE", value: business.Type.ToInt32(), direction: ParameterDirection.Input);
        return parameters;
    }

    private DynamicParameters Build(CreateUserCode business, long codeId) {
        var parameters = this.Build(business);
         parameters.Add(name: "@CODEID", value: codeId, direction: ParameterDirection.Input);
        return parameters;
    }

    private DynamicParameters Build(RemoveUserCodeRule business) {
        var parameters = new DynamicParameters();
        parameters.Add(name: "@AUTHID", value: business.User.UserId, direction: ParameterDirection.Input);
        parameters.Add(name: "@CODEID", value: business.Code.CodeId, direction: ParameterDirection.Input);
        return parameters;
    }
}
