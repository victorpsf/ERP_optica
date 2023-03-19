using static Application.Models.AuthorizationModels;

namespace Application.Repositories.Rules;

public static class PermissionRules
{
    public class VerifyPermissionRule
    {
        public LoggedUserDto loggedUserDto { get; set; } = new LoggedUserDto();
        public string Permission { get; set; } = string.Empty;
    }
}
