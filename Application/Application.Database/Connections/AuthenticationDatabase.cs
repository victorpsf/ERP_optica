using Application.Interfaces.Database;

namespace Application.Database.Connections;

public class AuthenticationDatabase: DbMysqlClient, IAuthenticationDatabase
{
    public AuthenticationDatabase(string ConnectionString) : base(ConnectionString) { }
    public AuthenticationDatabase(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}
