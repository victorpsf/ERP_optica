using static Application.Models.Security.JwtModels;

namespace Application.Interfaces;

public interface IJwtService
{
    void Write(ClaimIdentifier claim, out TokenCreated output);
    void Read(string token, out ClaimIdentifier claim);
}
