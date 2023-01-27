using Authentication.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Shared.Services
{
    public class AppAuthorizationMiddleware : AuthorizationHandler<AuthorizationMiddlewareRequirements>
    {
        private readonly LoggedUser Authentication;
        private readonly AuthorizationRepository Repository;

        public AppAuthorizationMiddleware(LoggedUser Authentication, AuthorizationRepository Repository)
        {
            this.Repository = Repository;
            this.Authentication = Authentication;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationMiddlewareRequirements dependecy)
        {
            try
            {
                if (this.Repository.VerifyPermission(this.Authentication.Identifier, dependecy.Permission))
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
}
