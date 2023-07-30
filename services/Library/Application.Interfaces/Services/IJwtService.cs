using static Application.Base.Models.JwtModels;

namespace Application.Interfaces.Services;

public interface IJwtService
{
    void Write(ClaimIdentifier claim, out TokenCreated output);
    void Read(string token, out ClaimIdentifier claim);
}
