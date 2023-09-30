using Application.Interfaces.Utils;
using System.Linq;

namespace Application.Utils;

public class AppConfigurationManager: IAppConfigurationManager
{
    public IConfiguration configuration { get; set; }

    public AppConfigurationManager(IConfiguration configuration)
    { this.configuration = configuration; }

    public static AppConfigurationManager GetInstance(IConfiguration configuration) => new AppConfigurationManager(configuration);

    private string GetSeparator ()
    {
#if DEBUG
        return ":";
#elif RELEASE
        return "";
#endif
    }

    private string GetPathName(string[] path)
        => string.Join(
                this.GetSeparator(),
                path.Select(x =>
                {
#if DEBUG
                    return x;
#elif RELEASE
                    return $"{x.Substring(0, 1).ToUpperInvariant()}{x.Substring(1).ToLowerInvariant()}";
#endif
                })
            );

    public string GetProperty(params string[] path)
         => this.configuration.GetValue<string>(this.GetPathName(path)) ?? string.Empty;
}
