using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Application.Core {
    public static class ProgramCore {
        public static void ConfigureWebHostDefaults (IWebHostBuilder webhost) {
            webhost.UseKestrel(options => {
                options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(20));
            });
        } 
    }
}