using Application.Database.Interfaces;
using Authentication.Repositories.Queries;

namespace Authentication.Repositories;

public partial class AuthenticationRepository: AuthenticationSql
{
    private readonly IAuthenticationDatabase factory;

    public AuthenticationRepository(IAuthenticationDatabase factory)
    { this.factory = factory; }
}

