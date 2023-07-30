using static Application.Base.Models.JwtModels;

namespace Application.Interfaces.Services;

public interface ILoggedUser
{
    HttpContext? context { get; }
    LoggedUserDto Identifier { get; }
}
