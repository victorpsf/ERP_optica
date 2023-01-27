using Dapper;
using System.Data;
using static Application.Library.AuthenticationModels;
using static Application.Library.DatabaseModels;

namespace Authentication.Repositories
{
    public partial class AuthorizationRepository
    {
        public bool VerifyPermission(LoggedUserDto loggedUser, string Permission)
        {
            var parameters = new DynamicParameters();

            parameters.Add(name: "@USERID", value: loggedUser.UserId, direction: ParameterDirection.Input);
            parameters.Add(name: "@ENTERPRISEID", value: loggedUser.EnterpriseId, direction: ParameterDirection.Input);
            parameters.Add(name: "@PERMISSIONNAME", value: Permission, direction: ParameterDirection.Input);

            this.factory.Connect();
            int quantity = this.factory.Find<int>(new BancoArgument
            {
                Sql = AuthorizatedSql,
                Parameter = parameters,
                CmdType = (int)CommandType.Text
            });
            this.factory.Disconnect();
            return quantity > 0;
        }
    }
}
