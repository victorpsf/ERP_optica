using Application.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Application.Middleware;

public class PermissionRequirements: IAuthorizationRequirement
{
    public string Permission { get; } = string.Empty;

    public PermissionRequirements(string Permission)
    { this.Permission = Permission; }
}

public class Permission: AuthorizationHandler<PermissionRequirements>
{
    private readonly LoggedUser Authentication;
    private readonly IPermissionRepository Repository;

    public Permission(LoggedUser Authentication, IPermissionRepository Repository)
    {
        this.Repository = Repository;
        this.Authentication = Authentication;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirements dependecy)
    {
        try
        {
            context.Fail();
            //if (this.Repository.VerifyPermission(this.Authentication.Identifier, dependecy.Permission))
            //    context.Succeed(requirement: dependecy);
            //else
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
