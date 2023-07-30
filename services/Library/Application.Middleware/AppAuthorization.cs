using Application.Interfaces.RepoServices;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using static Application.Business.Rules.AppAuthorizationRepoServiceModels;

namespace Application.Middleware;

public class AppAuthorization: AuthorizationHandler<AppAuthorizationRequirements>
{
    private readonly ILoggedUser loggedUser;
    private readonly IAppLogger appLogger;
    private readonly IAppAuthorizationRepoService appAuthorizationRepoService;

    public AppAuthorization(ILoggedUser loggedUser, IAppLogger appLogger, IAppAuthorizationRepoService appAuthorizationRepoService)
    {
        this.loggedUser = loggedUser;
        this.appLogger = appLogger;
        this.appAuthorizationRepoService = appAuthorizationRepoService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppAuthorizationRequirements dependecy)
    {
        try
        {
            if (this.appAuthorizationRepoService.VerifyPermission(new VerifyPermissionRule { loggedUser = this.loggedUser.Identifier, Permission = dependecy.Permission }))
                context.Succeed(requirement: dependecy);
            else
                context.Fail();
        }

        catch (Exception ex)
        {
            this.appLogger.PrintsTackTrace(ex);
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
