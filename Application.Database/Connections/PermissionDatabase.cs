using Application.Interfaces.Database;

namespace Application.Database.Connections
{
    public class PermissionDatabase: DbMysqlClient ,IPermissionDatabase
    {
        public PermissionDatabase(string ConnectionString) : base(ConnectionString) { }
        public PermissionDatabase(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
    }
}
