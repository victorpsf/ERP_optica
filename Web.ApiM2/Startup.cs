using Application.Core;
using Application.Database;
using Application.Database.Interfaces;
using Web.ApiM2.Repositories;

namespace Web.ApiM2
{
    public class Startup : StartupCore
    {
        public Startup(IConfiguration configuration) : base(configuration, AnotherServices) { }

        public static void AnotherServices(IServiceCollection services, StartupCore context, IConfiguration configuration)
        {
            services.AddScoped<IM2Database, M2Database>(options => DatabaseFactory.M2Database(configuration));
            services.AddScoped<PersonPhysicalRepository>();
            services.AddScoped<PersonDocumentRepository>();
            services.AddScoped<PersonContactRepository>();
            services.AddScoped<PersonAddressRepository>();
        }
    }
}
