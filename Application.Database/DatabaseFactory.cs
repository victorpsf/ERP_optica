using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using static Application.Library.DatabaseModels;

namespace Application.Database;

public class DatabaseFactory {
    public static AuthenticationDatabase CreateAuthenticationDatabase (IConfiguration configuration) 
        => new AuthenticationDatabase(configuration.GetSection("ConnectionStrings:MySqlConnectionAuthentication").Value ?? string.Empty, 22000);
}