using Application.Database.Interfaces;

namespace Application.Database
{
    public class TestDatabase: DbMysqlClient, ITestDatabase
    {
        public TestDatabase(string ConnectionString) : base(ConnectionString) { }
        public TestDatabase(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
    }
}
