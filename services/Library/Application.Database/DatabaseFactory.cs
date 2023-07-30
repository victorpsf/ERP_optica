using Application.Database.Connections;
using Application.Interfaces.Utils;
using Microsoft.Extensions.Configuration;
using static Application.Base.Models.ConfigurationModels;

namespace Application.Database;

public class DatabaseFactory
{
    private IAppConfigurationManager appConfigurationManager;

    public DatabaseFactory(IAppConfigurationManager appConfigurationManager)
    {
        this.appConfigurationManager = appConfigurationManager;
    }

    private string GetConnectionString(string Identifier)
        => this.appConfigurationManager.GetProperty("Connections", "Database", Identifier, "ConnectionString");

    private int GetTimeout(string Identifier)
    {
        string value = this.appConfigurationManager.GetProperty("Connections", "Database", Identifier, "Timeout");

        try
        { return Convert.ToInt32(value); }

        catch
        { return 22000; }
    }

    public AuthenticationDatabase AuthenticationDatabaseConnection()
        => new AuthenticationDatabase(this.GetConnectionString("AuthenticationDatabase"), this.GetTimeout("AuthenticationDatabase"));

    public AuthorizationDatabase AuthorizationDatabaseConnection()
    => new AuthorizationDatabase(this.GetConnectionString("AuthorizationDatabase"), this.GetTimeout("AuthorizationDatabase"));

    public static DatabaseFactory GetInstance(IAppConfigurationManager appConfigurationManager) 
        => new DatabaseFactory(appConfigurationManager);
}
