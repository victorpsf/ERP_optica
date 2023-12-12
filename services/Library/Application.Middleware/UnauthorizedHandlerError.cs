using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Net;
using static Application.Base.Models.ControllerBaseModels;

namespace Application.Middleware;

public class UnauthorizedHandlerError
{
    public static async Task InvokeAsync(JwtBearerChallengeContext context)
    {
        context.HandleResponse();

        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        await context.Response.WriteAsJsonAsync(new RequestResult<object>
        {
            Errors = new List<ValidationError>
                {
                    new ValidationError
                    {
                        Propertie = "Client",
                        Message = "This route require authentication"
                    }
                }
        });
    }
}
