using Application.Interfaces;
using Application.Interfaces.Database;
using Application.Middleware;
using Application.Database.Connections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Repository.Interfaces;
using Application.Repositories.Security;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.Library;
using Microsoft.AspNetCore.Authorization;
using Application.Models.Security;

namespace Application.Core;

public delegate void ConfigureAnotherServices(IServiceCollection services, StartupCore context);

public class StartupCore
{
    public IConfiguration Configuration { get; }
    private readonly ConfigureAnotherServices? AnotherServices;

    public StartupCore(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public StartupCore(IConfiguration configuration, ConfigureAnotherServices anotherServices): this(configuration)
    {
        this.AnotherServices = anotherServices;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserLanguage, UserLanguage>();

        services.AddScoped<IAuthorizationDatabase, AuthorizationDatabase>(options => MysqlConnectionFactory.AuthorizationDatabase(this.Configuration));
        services.AddScoped<IPermissionDatabase, PermissionDatabase>(options => MysqlConnectionFactory.PermissionDatabase(this.Configuration));

        services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ILoggedUser, LoggedUser>();

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
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(BinaryConverter.ToBytesView(this.Configuration.GetSection(JwtService.GetEnv("Key")).Value ?? string.Empty, Models.Security.BinaryViewModels.BinaryView.HEX)),
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };
                }
            );

        services.AddScoped<IAuthorizationHandler, Permission>();
        services.AddAuthorization(
            x =>
            {
                x.FallbackPolicy = x.DefaultPolicy;
                foreach (string permissionName in PermissionModels.AllPermissions())
                    x.AddPolicy(permissionName, policy => policy.AddRequirements(new PermissionRequirements(permissionName)));
            }
        );

        if (this.AnotherServices is not null) this.AnotherServices(services, this);
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment environment)
    {
        app.UseRouting();
#if RELEASE
        app.UseHttpsRedirection();
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
