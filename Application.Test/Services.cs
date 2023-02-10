using Application.Database;
using Application.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test;

public class Services
{
    public Configuration TestConfig { get; }
    public TestRepository repository { get; }
    private ITestDatabase Database { get; }

    public Services(Configuration testConfig)
    {
        this.TestConfig = testConfig;
        this.Database = DatabaseFactory.TestDatabase(this.TestConfig.configuration);
        this.repository = new TestRepository(this.Database);
    }

    private HttpClient CreateClient(string BaseUrl)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(BaseUrl);
        client.Timeout = TimeSpan.FromSeconds(12);
        return client;
    }

    public HttpClient AuthenticationClient() => this.CreateClient(this.TestConfig.AuthenticationPath);
}
