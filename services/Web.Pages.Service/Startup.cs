using Application.Core;
using Application.Interfaces.Utils;
using static Application.Base.Models.ConfigurationModels;

namespace Web.Pages.Service;

public class Startup: StartupCore
{
    public Startup(IConfiguration configuration) : base(
        configuration: configuration,
        databaseNames: new List<DatabaseName> {
            DatabaseName.AUTHENTICATION,
            DatabaseName.AUTHORIZATION,
        },
        prefix: "pages",
        disableCors: true
    )
    { }

    public override void ConfigureAnotherServices(IServiceCollection services, IAppConfigurationManager configuration, StartupCore context)
    { }
}