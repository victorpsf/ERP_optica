using Application.Database;
using Authentication.Repositories.Queries;

namespace Authentication.Repositories
{
    public partial class AuthorizationRepository: AuthorizationSql
    {
        private readonly DbMysqlClientFactory factory;

        public AuthorizationRepository(DbMysqlClientFactory factory)
        { this.factory = factory; }
    }
}
