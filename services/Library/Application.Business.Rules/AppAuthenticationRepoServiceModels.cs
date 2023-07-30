using static Application.Base.Models.JwtModels;

namespace Application.Business.Rules;

public static class AppAuthenticationRepoServiceModels
{
    public class FindClaimUserRule
    {
        public ClaimIdentifier claim { get; set; } = new ClaimIdentifier();
    }
}
