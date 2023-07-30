using Application.Interfaces.Connections;

namespace Application.Database.Connection
{
    public class DbMysqlClient: DatabaseMysqlActions, IDBMysqlClient
    {
        public DbMysqlClient(string ConnectionString) : base(ConnectionString) { }
        public DbMysqlClient(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
    }
}
