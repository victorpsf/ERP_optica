using Application.Core;
using Application.Database;
using Application.Database.Interfaces;
using Web.ApiM1.Repositories;

namespace Web.ApiM1
{
    public class Startup : StartupCore
    {
        public Startup(IConfiguration configuration) : base(configuration, AnotherServices) { }

        public static void AnotherServices(IServiceCollection services, StartupCore context, IConfiguration configuration)
        {
            services.AddScoped<IM1Database, M1Database>(options => DatabaseFactory.M1Database(configuration));
            services.AddScoped<EnterpriseRepository>();
        }
    }
}
