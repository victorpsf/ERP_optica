using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Application.Middleware;

public class UserLanguage: IUserLanguage
{
    public HttpContext? context { get; }
    //public LanguageEnum language { get; }

    public UserLanguage(IHttpContextAccessor httpContextAcessor)
    {
        this.context = httpContextAcessor.HttpContext;

        //if (this.context == null)
        //{
        //    this.language = LanguageEnum.PTBR;
        //}

        //else
        //    this.language = MultiLanguage.GetLanguage(this.context.Request.Headers["location"].FirstOrDefault<string>() ?? string.Empty);
    }
}
