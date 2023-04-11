using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Application.Database.Connections;

public class MysqlConnectionFactory
{
    private static string GetEnvKey(string Identifier)
    {
#if DEBUG
        return $"ConnectionStrings:{Identifier}";
#elif RELEASE
        return $"Env{Identifier}";
#endif
    }

    public static AuthorizationDatabase AuthorizationDatabase(IConfiguration configuration)
    {
        Console.WriteLine(configuration.GetSection(GetEnvKey("MySqlConnectionAuthorization")).Value);
        return new AuthorizationDatabase(configuration.GetSection(GetEnvKey("MySqlConnectionAuthorization")).Value ?? string.Empty, 22000);
    }

    public static PermissionDatabase PermissionDatabase(IConfiguration configuration)
        => new PermissionDatabase(configuration.GetSection(GetEnvKey("MySqlConnectionPermission")).Value ?? string.Empty, 22000);
}
