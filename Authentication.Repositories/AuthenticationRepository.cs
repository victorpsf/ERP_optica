using Application.Database;
using Authentication.Repositories.Queries;

namespace Authentication.Repositories;

public partial class AuthenticationRepository: AuthenticationSql
{
    private readonly DbMysqlClientFactory factory;

    public AuthenticationRepository(DbMysqlClientFactory factory)
    { this.factory = factory; }
}

