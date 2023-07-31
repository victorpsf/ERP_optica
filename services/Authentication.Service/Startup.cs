using Application.Core;
using Application.Interfaces.Utils;
using Authentication.Service.Repositories;
using Authentication.Service.Repositories.Services;
using static Application.Base.Models.ConfigurationModels;

namespace Authentication.Service;

public class Startup: StartupCore
{
    public Startup(IConfiguration configuration): base(
        configuration: configuration, 
        databaseNames: new List<DatabaseName> { 
            DatabaseName.AUTHENTICATION, 
            DatabaseName.AUTHORIZATION,
            DatabaseName.AUTHENTICATE
        },
        prefix: "auth",
        disableCors: true
    )
    { }

    public override void ConfigureAnotherServices (IServiceCollection services, IAppConfigurationManager configuration, StartupCore context)
    {
        services.AddScoped<AuthenticateRepository>();
        services.AddScoped<AuthenticateRepoService>();
    }
}
