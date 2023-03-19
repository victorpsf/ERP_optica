using static Application.Library.AuthenticationModels;
using static Authentication.Repositories.Rules.AuthenticationRules;

namespace Application.Interfaces.Repositories;

public interface IAuthenticationRepository
{
    UserCodeDto Save(CreateUserCode business);
    void Remove(RemoveUserCodeRule business);
    UserDto Find(FindUserRule business);
    UserCodeDto? Find(FindUserCodeRule business);
}
