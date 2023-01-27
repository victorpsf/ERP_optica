using Microsoft.AspNetCore.Authorization;

namespace Shared.Services
{
    public class AuthorizationMiddlewareRequirements: IAuthorizationRequirement
    {
        public string Permission { get; } = string.Empty;

        public AuthorizationMiddlewareRequirements(string Permission)
        { this.Permission = Permission; }
    }
}
