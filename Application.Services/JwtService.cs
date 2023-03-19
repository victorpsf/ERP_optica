using Application.Interfaces;
using Application.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Application.Models.Security.JwtModels;

namespace Application.Services;

public class JwtService: IJwtService
{
    private readonly IConfiguration Configuration;

    public JwtService(IConfiguration Configuration)
    { this.Configuration = Configuration; }

    public static string GetEnv(string Identifier)
    {
#if DEBUG
        return $"Secret:{Identifier}";
#elif RELEASE
        return $"EnvSecret{Identifier}";
#endif
    }

    private byte[] GetTokenSecret()
    { return BinaryConverter.ToBytesView(this.Configuration.GetSection(GetEnv("Key")).Value ?? "", Models.Security.BinaryViewModels.BinaryView.BASE64); }

    private int GetTotalMinutes()
    {
        try { return Convert.ToInt32(this.Configuration.GetSection(GetEnv("Minutes")).Value); }
        catch { return 60; }
    }

    public void Write(ClaimIdentifier claim, out TokenCreated output)
    {
        output = new TokenCreated { Expire = DateTime.UtcNow.AddMinutes(this.GetTotalMinutes()) };

        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, $"{claim.UserId}:{claim.EnterpriseId}") }),
            Expires = output.Expire,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.GetTokenSecret()), SecurityAlgorithms.HmacSha256Signature)
        };
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        output.Token = handler.WriteToken(handler.CreateToken(descriptor));
    }

    public void Read(string token, out ClaimIdentifier claim)
    {
        try
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(this.GetTokenSecret()),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwt = (JwtSecurityToken)validatedToken;
            claim = ClaimIdentifier.Create(jwt.Claims.Where(a => a.Type == "nameid").FirstOrDefault()?.Value ?? string.Empty);
        }
        catch (Exception error)
        { throw new Exception($"INVALID TOKEN {error.Message}", error); }
    }
}
