using Microsoft.Extensions.Configuration;
using Mysqlx.Expr;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Application.Database.Connections;

public class MysqlConnectionFactory
{
#if DEBUG
    private static string prefix = "ConnectionStrings:";
#elif RELEASE
    private static string prefix = "Env";
#endif


    private static string GetConnectionString(IConfiguration configuration, string Identifier)
        => configuration.GetSection($"{prefix}{Identifier}").Value ?? string.Empty;

    private static int GetTimeout(IConfiguration configuration) 
    {
        string value = configuration.GetSection($"{prefix}ConnectionTimeout").Value ?? string.Empty;

        try
        { return Convert.ToInt32(value); }

        catch
        { return 22000; }
    }

    public static AuthenticationDatabase AuthenticationDatabase(IConfiguration configuration)
        => new AuthenticationDatabase(GetConnectionString(configuration, "MySqlConnectionAuthentication"), GetTimeout(configuration));

    public static AuthorizationDatabase AuthorizationDatabase(IConfiguration configuration) =>
        new AuthorizationDatabase(GetConnectionString(configuration, "MySqlConnectionAuthorization"), GetTimeout(configuration));
}
