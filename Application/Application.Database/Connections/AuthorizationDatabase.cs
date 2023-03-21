using Application.Interfaces.Database;

namespace Application.Database.Connections;

public class AuthorizationDatabase: DbMysqlClient, IAuthorizationDatabase
{
    public AuthorizationDatabase(string ConnectionString) : base(ConnectionString) { }
    public AuthorizationDatabase(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}
