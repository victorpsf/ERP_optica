using Application.Database.Interfaces;

namespace Application.Database;

public class M1Database : DbMysqlClient, IM1Database
{
    public M1Database(string ConnectionString) : base(ConnectionString) { }
    public M1Database(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}
