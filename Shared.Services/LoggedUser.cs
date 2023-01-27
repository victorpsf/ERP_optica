using Authentication.Repositories;
using Microsoft.AspNetCore.Http;
using static Application.Library.AuthenticationModels;
using static Application.Library.JwtModels;

namespace Shared.Services
{
    public class LoggedUser
    {
        private readonly AuthenticationRepository repository;
        private readonly JwtService jwtService;
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
                    this.identifier = this.repository.Find(claim);

                    if (this.identifier is null) throw new Exception("Unautorized");
                }

                return this.identifier;
            }
        }

        public LoggedUser(IHttpContextAccessor httpContextAcessor, AuthenticationRepository repository, JwtService jwtService)
        {
            this.repository = repository;
            this.jwtService = jwtService;

            this.context = httpContextAcessor.HttpContext;
        }
    }
}
