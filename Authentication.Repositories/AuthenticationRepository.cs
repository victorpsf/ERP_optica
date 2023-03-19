using Application.Database.Interfaces;
using Application.Interfaces.Repositories;
using Authentication.Repositories.Queries;

namespace Authentication.Repositories;

public partial class AuthenticationRepository: AuthenticationSql, IAuthenticationRepository
{
    private readonly IAuthenticationDatabase factory;

    public AuthenticationRepository(IAuthenticationDatabase factory)
    { this.factory = factory; }
}

