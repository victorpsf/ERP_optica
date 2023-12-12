using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using static Application.Base.Models.ControllerBaseModels;

namespace Application.Middleware;

public class StatusCodeHandleError
{
    public static async Task InvokeAsync(StatusCodeContext context)
    {
        if (context.HttpContext.Response.StatusCode == 403)
        {
            await context.HttpContext.Response.WriteAsJsonAsync(new RequestResult<object>
            {
                Errors = new List<ValidationError>
                {
                    new ValidationError
                    {
                        Propertie = "Client",
                        Message = "You need authorization to access this method"
                    }
                }
            });
        }
    }
}
