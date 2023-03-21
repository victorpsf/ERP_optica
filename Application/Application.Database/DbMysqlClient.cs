using Application.Interfaces.Database;

namespace Application.Database;

public class DbMysqlClient: DatabaseMysqlActions, IDBMysqlClient
{
    public DbMysqlClient(string ConnectionString) : base(ConnectionString) { }
    public DbMysqlClient(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}
