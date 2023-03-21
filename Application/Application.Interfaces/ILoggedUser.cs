using Microsoft.AspNetCore.Http;
using static Application.Models.AuthorizationModels;

namespace Application.Interfaces;

public interface ILoggedUser
{
    HttpContext? context { get; }
    LoggedUserDto Identifier { get; }
}
