using Application.Core;
using Application.Utils;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System.Globalization;

namespace Personal.Service;

public static class Program
{
    public static int Main(string[] arguments)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            CreateHost(arguments).Build().Run();
            return 0;
        }

        catch (Exception ex)
        {
            Log.Fatal("FALHA :: " + ex.Message);
            return 1;
        }
    }

    public static IHostBuilder CreateHost(string[] arguments) =>
        Host.CreateDefaultBuilder(arguments)
            .UseSerilog()
            .ConfigureWebHostDefaults(webhost => {
                ProgramCore.ConfigureWebHostDefaults(webhost);
                webhost.UseStartup<Startup>();
            });
}