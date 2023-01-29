using Application.Database.Interfaces;

namespace Application.Database;

public class M2Database: DbMysqlClient, IM2Database
{
    public M2Database(string ConnectionString) : base(ConnectionString) { }
    public M2Database(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}
