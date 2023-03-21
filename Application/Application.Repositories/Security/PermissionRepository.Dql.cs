using static Application.Models.Database.DatabaseModels;
using static Application.Repositories.Rules.PermissionRules;
using System.Data;

namespace Application.Repositories.Security;

public partial class PermissionRepository
{
    public bool VerifyPermission (VerifyPermissionRule rule)
    {
        this.db.Connect();

        var authorized = this.db.Find<bool>(new BancoArgument
        {
            Sql = this.VerifyPermissionSql,
            Parameter = ParameterCollection.GetInstance()
                .Add(field: "@USERID", value: rule.loggedUserDto.UserId, direction: ParameterDirection.Input)
                .Add(field: "@ENTERPRISEID", value: rule.loggedUserDto.EnterpriseId, direction: ParameterDirection.Input)
                .Add(field: "@PERMISSIONNAME", value: rule.Permission, direction: ParameterDirection.Input)
        });

        this.db.Disconnect();

        return authorized;
    }
}
