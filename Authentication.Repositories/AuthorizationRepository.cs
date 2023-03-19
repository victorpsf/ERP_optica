using Application.Database.Interfaces;
using Application.Interfaces.Repositories;
using Authentication.Repositories.Queries;

namespace Authentication.Repositories
{
    public partial class AuthorizationRepository: AuthorizationSql, IAuthorizationRepository
    {
        private readonly IPermissionDatabase factory;

        public AuthorizationRepository(IPermissionDatabase factory)
        { this.factory = factory; }
    }
}
