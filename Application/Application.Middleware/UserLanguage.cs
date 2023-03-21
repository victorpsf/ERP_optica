using Application.Interfaces;
using Application.Mensagens;
using Application.Services;
using Microsoft.AspNetCore.Http;
using static Application.Models.MultiLanguageModels;

namespace Application.Middleware;

public class UserLanguage: IUserLanguage
{
    public HttpContext? context { get; }
    public LanguageEnum language { get; }
    public IMessage message { get; }

    public UserLanguage(IHttpContextAccessor httpContextAcessor)
    {
        this.context = httpContextAcessor.HttpContext;
        var value = this.context is not null ? this.context.Request.Headers["location"].FirstOrDefault<string>() ?? string.Empty : string.Empty;
        this.language = MultiLanguage.GetLanguage(value);
        this.message = Message.Create(this.language);
    }
}
