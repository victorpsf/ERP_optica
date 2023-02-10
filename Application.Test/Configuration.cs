using Microsoft.Extensions.Configuration;

namespace Application.Test;

public class Configuration
{
    public IConfiguration configuration { get; }
    public string Token { get; set; } = string.Empty;
    public string AuthenticationPath { get; } = string.Empty;

    public Configuration()
    {
        this.configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

        this.AuthenticationPath = this.configuration.GetValue<String>("Services:AuthenticationPath") ?? string.Empty;
        //this.AuthenticationPath = this.configuration.GetValue<String>("") ?? string.Empty;
        //this.AuthenticationPath = this.configuration.GetValue<String>("") ?? string.Empty;
        //this.AuthenticationPath = this.configuration.GetValue<String>("") ?? string.Empty;
    }

    public void SetToken (string token) => this.Token = token;
}
