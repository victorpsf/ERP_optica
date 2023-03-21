using Application.Interfaces;
using Application.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using static Application.Models.AuthorizationModels;
using static Application.Models.Security.JwtModels;

namespace Application.Middleware;

public class LoggedUser: ILoggedUser
{
    private readonly IAuthorizationRepository repository;
    private readonly IJwtService jwtService;

    public HttpContext? context { get; }
    private LoggedUserDto? identifier;

    public LoggedUserDto Identifier
    {
        get
        {
            if (this.context is null) throw new Exception("Unautorized");
            if (this.identifier is null)
            {
                string token = this.context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last<string>() ?? string.Empty;

                this.jwtService.Read(token, out ClaimIdentifier claim);
                //this.identifier = this.repository.Find(claim);

                if (this.identifier is null) throw new Exception("Unautorized");
            }

            return this.identifier;
        }
    }

    public LoggedUser(IHttpContextAccessor httpContextAcessor, IAuthorizationRepository repository, IJwtService jwtService)
    {
        this.repository = repository;
        this.jwtService = jwtService;

        this.context = httpContextAcessor.HttpContext;
    }
}
