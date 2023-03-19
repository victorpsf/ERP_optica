using static Application.Library.AuthenticationModels;
using static Application.Library.JwtModels;

namespace Application.Interfaces.Repositories;

public interface IAuthorizationRepository
{
    bool VerifyPermission(LoggedUserDto loggedUser, string Permission);
    LoggedUserDto? Find(ClaimIdentifier claim);
}
