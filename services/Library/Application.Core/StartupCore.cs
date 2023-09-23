using Application.Base.Models;
using Application.Database;
using Application.Database.Connections;
using Application.Interfaces.Connections;
using Application.Interfaces.Middleware;
using Application.Interfaces.RepoServices;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.Utils;
using Application.Middleware;
using Application.Repositories;
using Application.Repositories.Services;
using Application.Services;
using Application.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using static Application.Base.Models.ConfigurationModels;

namespace Application.Core;

public class StartupCore
{
    protected IConfiguration configuration;
    protected List<DatabaseName> databaseNames = new List<DatabaseName>();

    public IAppConfigurationManager configuationManager;
    public string? prefix;
    public bool disableCors = false;
    public string? origin;
    public AppBackgroundJob backgroundJob;

    private string Prefix { get => this.prefix is not null ? this.prefix : "api"; }
    private string Pattern { get => (this.Prefix == "api" ? "/api" : $"/{this.Prefix}") + "/{controller}/{action}"; }
    private bool EnableCors { get => !this.disableCors && this.origin is not null; }

    private byte[] Secret
    {
        get
        {
            var secret = this.configuationManager.GetProperty("Security", "Jwt", "Secret");
            return BinaryManager.From(secret, Base.Models.BinaryManagerModels.BinaryView.Base64).Binary;
        }
    }

    public StartupCore(IConfiguration configuration)
    { 
        this.configuration = configuration;
        this.configuationManager = AppConfigurationManager.GetInstance(configuration);
        this.backgroundJob = new AppBackgroundJob();
    }

    public StartupCore(IConfiguration configuration, List<DatabaseName> databaseNames): this(configuration)
    { this.databaseNames = databaseNames; }

    public StartupCore(IConfiguration configuration, List<DatabaseName> databaseNames, string prefix) : this(configuration, databaseNames)
    { this.prefix = prefix; }

    public StartupCore(IConfiguration configuration, List<DatabaseName> databaseNames, string prefix, bool disableCors) : this(configuration, databaseNames, prefix)
    { this.disableCors = disableCors; }

    public StartupCore(IConfiguration configuration,  List<DatabaseName> databaseNames, string prefix, bool disableCors, string origin) : this(configuration, databaseNames, prefix, disableCors)
    { this.origin = origin; }

    public virtual void ConfigureAnotherServices(IServiceCollection services, IAppConfigurationManager configuration, StartupCore context)
    { }

    private void configureDatabases(IServiceCollection services)
    {
        foreach (DatabaseName databaseName in this.databaseNames)
        {
            var factory = DatabaseFactory.GetInstance(this.configuationManager);
            switch (databaseName)
            {
                case DatabaseName.AUTHENTICATION:
                    services.AddScoped<IAuthenticationDatabase, AuthenticationDatabase>(options => factory.AuthenticationDatabaseConnection());
                    services.AddScoped<IAppAuthenticationRepository, AppAuthenticationRepository>();
                    services.AddScoped<IAppAuthenticationRepoService, AppAuthenticationRepoService>();
                    break;
                case DatabaseName.AUTHORIZATION:
                    services.AddScoped<IAuthorizationDatabase, AuthorizationDatabase>(options => factory.AuthorizationDatabaseConnection());
                    services.AddScoped<IAppAuthorizationRepository, AppAuthorizationRepository>();
                    services.AddScoped<IAppAuthorizationRepoService, AppAuthorizationRepoService>();
                    break;
                case DatabaseName.AUTHENTICATE:
                    services.AddScoped<IAuthenticateDatabase, AuthenticateDatabase>(options => factory.AuthenticateDatabaseConnection());
                    break;
            }

        }
    }

    private void configureAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(
                x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(this.Secret),
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };
                }
            );
    }

    private void configureAuthorization(IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, AppAuthorization>();
        services.AddAuthorization(
            x =>
            {
                x.FallbackPolicy = x.DefaultPolicy;
                foreach (string permissionName in PermissionModels.AllPermissions())
                    x.AddPolicy(permissionName, policy => policy.AddRequirements(new AppAuthorizationRequirements(permissionName)));
            }
        );
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var temporaryCache = new AppHostTemporaryCache();
        if (this.EnableCors)
            services.AddCors(options =>
                options.AddPolicy("_myAllowSpecificOrigins", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithOrigins(this.origin ?? "");
                })
            );

        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAppConfigurationManager, AppConfigurationManager>();
        services.AddScoped<IAppLogger, AppLogger>();
        services.AddScoped<ISmtpService, SmtpService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<IAttributeValidationBase, AttributeValidationBase>();
        services.AddScoped<IBaseControllerServices, BaseControllerServices>();
        services.AddSingleton(temporaryCache);
        services.AddScoped<IHostCache, HostCache>();

        this.backgroundJob.RegistryJob(() => temporaryCache.UnsetValue());
        this.backgroundJob.On(1000);

        this.configureDatabases(services);
        this.configureAuthentication(services);
        this.configureAuthorization(services);
        this.ConfigureAnotherServices(services, this.configuationManager, this);
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseRouting();
#if RELEASE
        app.UseHttpsRedirection();
#endif
        if (this.EnableCors)
            app.UseCors("_myAllowSpecificOrigins");
        else
            app.UseCors(a => a.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseAuthentication();
        app.UseAuthorization();

        if (env.IsDevelopment()) { }

        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllers();
            endpoint.MapControllerRoute(
                name: this.Prefix,
                pattern: this.Pattern
            );
        });
    }
}
