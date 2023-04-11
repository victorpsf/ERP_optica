using Microsoft.AspNetCore.Http;
using static Application.Models.MultiLanguageModels;

namespace Application.Interfaces;

public interface IUserLanguage
{
    public HttpContext? context { get; }
    public LanguageEnum language { get; }
    public IMessage message { get; }
}
