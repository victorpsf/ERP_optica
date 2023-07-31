using Application.Database.Connection;
using Application.Interfaces.Connections;

namespace Application.Database.Connections;

public class AuthenticateDatabase : DbMysqlClient, IAuthenticateDatabase
{
    public AuthenticateDatabase(string ConnectionString) : base(ConnectionString) { }
    public AuthenticateDatabase(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}
