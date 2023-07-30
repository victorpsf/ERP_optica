using Microsoft.AspNetCore.Authorization;

namespace Application.Middleware;

public class AppAuthorizationRequirements: IAuthorizationRequirement
{
    public string Permission { get; } = string.Empty;

    public AppAuthorizationRequirements(string Permission)
    { this.Permission = Permission; }
}
