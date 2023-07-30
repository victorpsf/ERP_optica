using static Application.Base.Models.JwtModels;

namespace Application.Business.Rules;

public static class AppAuthorizationRepoServiceModels
{
    public class VerifyPermissionRule
    {
        public LoggedUserDto loggedUser { get; set; } = new LoggedUserDto();
        public string Permission { get; set; } = string.Empty;
    }
}
