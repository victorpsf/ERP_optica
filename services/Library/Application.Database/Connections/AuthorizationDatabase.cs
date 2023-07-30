using Application.Database.Connection;
using Application.Interfaces.Connections;

namespace Application.Database.Connections;

public class AuthorizationDatabase: DbMysqlClient, IAuthorizationDatabase
{
    public AuthorizationDatabase(string ConnectionString) : base(ConnectionString) { }
    public AuthorizationDatabase(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}
