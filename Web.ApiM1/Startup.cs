using Application.Core;
using Web.ApiM1.Repositories;

namespace Web.ApiM1
{
    public class Startup : StartupCore
    {
        public Startup(IConfiguration configuration) : base(configuration, AnotherServices) { }

        public static void AnotherServices(IServiceCollection services, StartupCore context, IConfiguration configuration)
        {
            services.AddScoped<EnterpriseRepository>();
        }
    }
}
