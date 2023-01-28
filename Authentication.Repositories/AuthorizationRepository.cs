using Application.Database.Interfaces;
using Authentication.Repositories.Queries;

namespace Authentication.Repositories
{
    public partial class AuthorizationRepository: AuthorizationSql
    {
        private readonly IAuthenticationDatabase factory;

        public AuthorizationRepository(IAuthenticationDatabase factory)
        { this.factory = factory; }
    }
}
