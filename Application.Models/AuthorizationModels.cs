namespace Application.Models;

public static class AuthorizationModels
{
    public class LoggedUserDto
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
