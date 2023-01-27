namespace Application.Library
{
  public static class JwtModels
  {
    public class ClaimIdentifier
    {
      public string UserId { get; set; } = string.Empty;
      public string EnterpriseId { get; set; } = string.Empty;

      public int UserIdInt()
      { return Convert.ToInt32(UserId); }

      public int EnterpriseIdInt()
      { return Convert.ToInt32(EnterpriseId); }

      public static ClaimIdentifier Create(string claim)
      {
        var claims = claim.Split(":").ToArray();
        return new ClaimIdentifier
        {
          UserId = claims.FirstOrDefault() ?? string.Empty,
          EnterpriseId = claims.LastOrDefault() ?? string.Empty
        };
      }
    }

    public class TokenCreated
    {
      public DateTime Expire { get; set; }
      public string Token { get; set; } = string.Empty;
    }
  }
}