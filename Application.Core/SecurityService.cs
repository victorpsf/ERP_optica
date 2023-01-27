using Application.Library;
using Application.Library.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Shared.Services;

namespace Application.Core
{
    public class SecurityService: IServiceAssinature
    {
        public IConfiguration Configuration { get; }

        private SecurityService(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<JwtService>();
            services.AddScoped<LoggedUser>();

            services
                .AddAuthentication(
                    x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(BinaryConverter.ToBytesView(this.Configuration.GetSection("Secret:Key").Value ?? string.Empty, BinaryConverter.StringView.HEXADECIMAL)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                );

            services.AddScoped<IAuthorizationHandler, AppAuthorizationMiddleware>();
            services
                .AddAuthorization(
                    x =>
                    {
                        x.FallbackPolicy = x.DefaultPolicy;
                        foreach (string permissionName in PermissionModels.AllPermissions())
                            x.AddPolicy(permissionName, policy => policy.AddRequirements(new AuthorizationMiddlewareRequirements(permissionName)));
                    }
                );
        }

        public static SecurityService Instance(IConfiguration Configuration)
        { return new SecurityService(Configuration); }
    }
}
