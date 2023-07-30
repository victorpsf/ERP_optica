using Application.Interfaces.Messages;
using Application.Interfaces.RepoServices;
using Application.Interfaces.Services;
using Application.Messages;
using Microsoft.AspNetCore.Http;
using static Application.Base.Models.JwtModels;
using static Application.Base.Models.MultiLanguageModels;
using static Application.Business.Rules.AppAuthenticationRepoServiceModels;

namespace Application.Services;

public class LoggedUser : ILoggedUser
{
    private readonly IJwtService jwtService;
    private readonly IAppAuthenticationRepoService appAuthenticationRepoService;
    public HttpContext? context { get; }
    private LoggedUserDto? identifier;

    public LanguageEnum lang { get; private set; }
    public IMessage message { get; private set; }

    private void HandleException() => throw new Exception("Unautorized");

    private string Token
    {
        get => this.context?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last<string>() ?? string.Empty;
    }

    private string Language
    {
        get => this.context?.Request.Headers["Location"].FirstOrDefault() ?? "pt-br";
    }

    public LoggedUserDto Identifier
    {
        get
        {
            if (this.context is null) 
                this.HandleException();

            if (this.identifier is null)
            {
                this.jwtService.Read(this.Token, out ClaimIdentifier claim);
                this.identifier = this.appAuthenticationRepoService.Find(new FindClaimUserRule { claim = claim });
            }

            if (this.identifier is null)
                this.HandleException();

            return this.identifier ?? new LoggedUserDto();
        }
    }

    public LoggedUser(IHttpContextAccessor httpContextAcessor, IAppAuthenticationRepoService appAuthenticationRepoService, IJwtService jwtService)
    {
        this.context = httpContextAcessor.HttpContext;
        this.appAuthenticationRepoService = appAuthenticationRepoService;
        this.jwtService = jwtService;

        this.lang = MultiLanguage.GetLanguage(this.Language);
        this.message = Message.Create(this.lang);
    }
}
