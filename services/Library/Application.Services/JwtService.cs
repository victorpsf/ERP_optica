using Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static Application.Base.Models.JwtModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Interfaces.Utils;
using Application.Utils;

namespace Application.Services;

public class JwtService: IJwtService
{
    private readonly IAppConfigurationManager appConfigurationManager;

    public JwtService(IAppConfigurationManager appConfigurationManager)
    { 
        this.appConfigurationManager = appConfigurationManager;
    }

    private byte[] Secret
    {
        get
        {
            var secret = this.appConfigurationManager.GetProperty("Security", "Jwt", "Secret");
            return BinaryManager.From(secret, Base.Models.BinaryManagerModels.BinaryView.Base64).Binary;
        }
    }

    private int Minutes
    {
        get
        {
            var minutes = this.appConfigurationManager.GetProperty("Security", "Jwt", "Minutes");

            try { return Convert.ToInt32(minutes); }
            catch { return 60; }
        }
    }

    public void Write(ClaimIdentifier claim, out TokenCreated output)
    {
        output = new TokenCreated { Expire = DateTime.UtcNow.AddMinutes(this.Minutes) };

        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, $"{claim.UserId}:{claim.EnterpriseId}") }),
            Expires = output.Expire,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.Secret), SecurityAlgorithms.HmacSha256Signature)
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
                IssuerSigningKey = new SymmetricSecurityKey(this.Secret),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwt = (JwtSecurityToken)validatedToken;
            claim = ClaimIdentifier.Create(
                jwt.Claims.Where(a => a.Type == "nameid").FirstOrDefault()?.Value ?? string.Empty, 
                token
            );
        }
        catch (Exception error)
        { throw new Exception($"INVALID TOKEN {error.Message}", error); }
    }
}
