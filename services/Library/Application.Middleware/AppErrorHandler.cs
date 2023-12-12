using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using static Application.Base.Models.ControllerBaseModels;

namespace Application.Middleware;

public class AppErrorHandler
{
    private readonly RequestDelegate next;

    public AppErrorHandler(RequestDelegate next)
    { this.next = next; }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }

        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new RequestResult<object>
            {
                Errors = new List<ValidationError>
                {
                    new ValidationError
                    {
                        Propertie = "Server",
                        Message = "Internal server error"
                    }
                }
            });
        }
    }
}
