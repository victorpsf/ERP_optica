using Application.Messages;
using Authentication.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services;

public class UserLanguage
{
    public HttpContext? context { get; }
    public LanguageEnum language { get; }

    public UserLanguage(IHttpContextAccessor httpContextAcessor)
    {
        this.context = httpContextAcessor.HttpContext;

        if (this.context == null)
        {
            this.language = LanguageEnum.PTBR;
        }

        else
            this.language = MultiLanguage.GetLanguage(this.context.Request.Headers["location"].FirstOrDefault<string>() ?? string.Empty);
    }
}
