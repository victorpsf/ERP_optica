using Application.Core;
using Application.Database.Connections;
using Application.Interfaces.Database;
using System.Configuration;

namespace Service.Authentication;

public class Startup: StartupCore
{
    public Startup(IConfiguration configuration) : base(configuration, dependencyServices)
    { }

    public static void dependencyServices (IServiceCollection services, StartupCore context) { 
        services.AddScoped<IAuthenticationDatabase, AuthenticationDatabase>(
            options => MysqlConnectionFactory.AuthenticationDatabase(context.Configuration)
        );
    }
}
