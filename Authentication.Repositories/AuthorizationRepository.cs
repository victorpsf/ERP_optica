using Application.Database.Interfaces;
using Authentication.Repositories.Queries;

namespace Authentication.Repositories
{
    public partial class AuthorizationRepository: AuthorizationSql
    {
        private readonly IPermissionDatabase factory;

        public AuthorizationRepository(IPermissionDatabase factory)
        { this.factory = factory; }
    }
}
