using Application.Database.Interfaces;

namespace Application.Database;

public class PermissionDatabase: DbMysqlClient, IPermissionDatabase
{
    public PermissionDatabase(string ConnectionString): base(ConnectionString) { }
    public PermissionDatabase(string ConnectionString, int Timeout): base(ConnectionString, Timeout) { }
}