using Application.Interfaces.Messages;
using static Application.Base.Models.JwtModels;
using static Application.Base.Models.MultiLanguageModels;

namespace Application.Interfaces.Services;

public interface ILoggedUser
{
    HttpContext? context { get; }
    LoggedUserDto Identifier { get; }
    LanguageEnum lang { get; }
    IMessage message { get; }
}
