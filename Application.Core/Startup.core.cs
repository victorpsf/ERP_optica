namespace Application.Core
{
    public delegate void ConfigureAnotherServices(IServiceCollection services, StartupCore context, IConfiguration configuration);

    public class StartupCore
    {
        private readonly IConfiguration configuration;
        private ConfigureAnotherServices? anotherServices;

        public StartupCore(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public StartupCore(IConfiguration configuration, ConfigureAnotherServices anotherServices) : this(configuration)
        {
            this.anotherServices = anotherServices;
        }

        public void ConfigureServices(IServiceCollection services)
        {
#if RELEASE
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "_myAllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("localhost:3000");
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                    }
                );
            });
#endif
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            DatabaseService.Instance(this.configuration).ConfigureService(services);
            SecurityService.Instance(this.configuration).ConfigureService(services);

            if (this.anotherServices is not null)
                this.anotherServices(services, this, this.configuration);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseRouting();
#if RELEASE
            app.UseHttpsRedirection();
            app.UseCors("_myAllowSpecificOrigins");
#endif
            app.UseAuthentication();
            app.UseAuthorization();

            if (environment.IsDevelopment()) { }

            app.UseEndpoints(
                endpoint =>
                {
                    endpoint.MapControllers();
                    endpoint.MapControllerRoute(
                        name: "api",
                        pattern: "/{controller}/{action}"
                    );
                }
            );
        }
    }
}
