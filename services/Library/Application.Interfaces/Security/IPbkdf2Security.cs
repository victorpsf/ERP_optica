using static Application.Base.Models.SecurityModels;

namespace Application.Interfaces.Security;

public interface IPbkdf2Security
{
    string Write(string value, Pbkdf2HashDerivation hashDerivation);
    bool Verify(string derived, string value, Pbkdf2HashDerivation hashDerivation);
}
