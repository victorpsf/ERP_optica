using Microsoft.Extensions.Configuration;

namespace Application.Test;

public class Program
{
    private readonly Configuration TestConfig;

    public Program() { TestConfig = new Configuration(); }

    public static Program Create() => new Program();

    public Task<int> Handle()
    {
        string token = AuthenticationTest.Create(this.TestConfig).Execute();

        return Task.FromResult(0);
    }

    public static async Task<int> Main(string[] args) => await Create().Handle();
}