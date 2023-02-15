using Microsoft.Extensions.Configuration;

namespace Application.Test;

public class Program
{
    private readonly Configuration TestConfig;

    public Program() { TestConfig = new Configuration(); }

    public static Program Create() => new Program();

    public async Task<int> Handle()
    {
        string token = await AuthenticationTest.Create(this.TestConfig).Execute();

        return 0;
    }

    public static async Task<int> Main(string[] args) => await Create().Handle();
}