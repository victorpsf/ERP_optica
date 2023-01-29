using Application.Core;
using Application.Database;
using Application.Database.Interfaces;

namespace Authentication.Service
{
    public class Startup: StartupCore
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        private void ConfigureAnotherServices (IServiceCollection services, StartupCore context, IConfiguration configuration) {
            services.AddScoped<IAuthenticationDatabase, AuthenticationDatabase>(options => DatabaseFactory.CreateAuthenticationDatabase(configuration));
            services.AddScoped<AuthenticationRepository>();
        }
    }
}
