using Application.Database.Interfaces;

namespace Application.Database;

public class AuthenticationDatabase: DbMysqlClient, IAuthenticationDatabase
{
    public AuthenticationDatabase(string ConnectionString): base(ConnectionString) { }
    public AuthenticationDatabase(string ConnectionString, int Timeout): base(ConnectionString, Timeout) { }
}