using Application.Database.Connection;
using Application.Interfaces.Connections;

namespace Application.Database.Connections;

public class PersonalDatabase: DbMysqlClient, IPersonalDatabase
{
    public PersonalDatabase(string ConnectionString) : base(ConnectionString) { }
    public PersonalDatabase(string ConnectionString, int Timeout) : base(ConnectionString, Timeout) { }
}