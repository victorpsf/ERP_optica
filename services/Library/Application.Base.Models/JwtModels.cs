namespace Application.Base.Models;

public static class JwtModels
{
    public class ClaimIdentifier
    {
        public string UserId { get; set; } = string.Empty;
        public string EnterpriseId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public int UserIdInt()
        { return Convert.ToInt32(UserId); }

        public int EnterpriseIdInt()
        { return Convert.ToInt32(EnterpriseId); }

        public static ClaimIdentifier Create(string PrimarySid, string Sid, string token)
        {
            return new ClaimIdentifier
            {
                UserId = PrimarySid,
                EnterpriseId = Sid,
                Token = token
            };
        }
    }

    public class TokenCreated
    {
        public DateTime Expire { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public class LoggedUserDto
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
