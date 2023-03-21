using Application.Interfaces;
using Application.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using static Application.Repositories.Rules.PermissionRules;

namespace Application.Middleware;

public class PermissionRequirements: IAuthorizationRequirement
{
    public string Permission { get; } = string.Empty;

    public PermissionRequirements(string Permission)
    { this.Permission = Permission; }
}

public class Permission: AuthorizationHandler<PermissionRequirements>
{
    private readonly ILoggedUser Authentication;
    private readonly IPermissionRepository Repository;

    public Permission(ILoggedUser Authentication, IPermissionRepository Repository)
    {
        this.Repository = Repository;
        this.Authentication = Authentication;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirements dependecy)
    {
        try
        {
            if (this.Repository.VerifyPermission(new VerifyPermissionRule { loggedUserDto = this.Authentication.Identifier, Permission = dependecy.Permission }))
                context.Succeed(requirement: dependecy);
            else
                context.Fail();
        }

        catch (Exception error)
        {
            switch (error.Message)
            {
                case "Unautorized":
                default:
                    context.Fail();
                    break;
            }
        }
        return Task.CompletedTask;
    }
}
