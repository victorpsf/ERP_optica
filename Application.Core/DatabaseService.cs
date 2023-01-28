using Application.Database;
using Application.Database.Interfaces;
using Authentication.Repositories;

namespace Application.Core
{
    public class DatabaseService : IServiceAssinature
    {
        public IConfiguration Configuration { get; }

        private DatabaseService(IConfiguration Configuration)
        { this.Configuration = Configuration; }

        public void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationDatabase, AuthenticationDatabase>(options => DatabaseFactory.CreateAuthenticationDatabase(this.Configuration));
            services.AddScoped<AuthenticationRepository>();
            services.AddScoped<AuthorizationRepository>();
        }

        public static DatabaseService Instance(IConfiguration Configuration) => new DatabaseService(Configuration);
    }
}
