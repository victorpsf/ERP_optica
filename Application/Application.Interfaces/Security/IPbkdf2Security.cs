using static Application.Models.Security.KeyDerivationModels;

namespace Application.Interfaces.Security;

public interface IPbkdf2Security
{
    string Write(string value, HashDerivation hashDerivation);
    bool Verify(string derived, string value, HashDerivation hashDerivation);
}
