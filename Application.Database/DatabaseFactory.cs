using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using static Application.Library.DatabaseModels;

namespace Application.Database;

public class DatabaseFactory {
    public static AuthenticationDatabase CreateAuthenticationDatabase (IConfiguration configuration) 
        => new AuthenticationDatabase(configuration.GetSection("ConnectionStrings:MySqlConnectionAuthentication").Value ?? string.Empty, 22000);

    public static PermissionDatabase CreatePermissionDatabase (IConfiguration configuration) 
        => new PermissionDatabase(configuration.GetSection("ConnectionStrings:MySqlConnectionPermission").Value ?? string.Empty, 22000);

    public static M1Database M1Database(IConfiguration configuration)
        => new M1Database(configuration.GetSection("ConnectionStrings:MySqlConnectionM1").Value ?? string.Empty, 22000);

    public static M2Database M2Database(IConfiguration configuration)
        => new M2Database(configuration.GetSection("ConnectionStrings:MySqlConnectionM2").Value ?? string.Empty, 22000);
}