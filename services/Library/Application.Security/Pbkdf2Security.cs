using Application.Interfaces.Security;
using Application.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using static Application.Base.Models.BinaryManagerModels;
using static Application.Base.Models.SecurityModels;

namespace Application.Security;

public class Pbkdf2Security: IPbkdf2Security
{
    private static int numBytes = 8192;

    public Pbkdf2Security() { }

    private KeyDerivationPrf Convert(Pbkdf2HashDerivation hashDerivation) => hashDerivation switch
    {
        Pbkdf2HashDerivation.HMACSHA512 => KeyDerivationPrf.HMACSHA512,
        Pbkdf2HashDerivation.HMACSHA256 => KeyDerivationPrf.HMACSHA256,
        Pbkdf2HashDerivation.HMACSHA1 => KeyDerivationPrf.HMACSHA1,
        _ => throw new ArgumentException($"[ERROR HashDerivation] Pbkdf2Security: {hashDerivation.ToString()} is not defined")
    };

    private byte[] DeriveValue(string value, byte[] salt, Pbkdf2HashDerivation hashDerivation) =>
        KeyDerivation.Pbkdf2(password: value, salt: salt, prf: Convert(hashDerivation), iterationCount: value.Length * 10, numBytesRequested: numBytes);

    public string Write(string value, Pbkdf2HashDerivation hashDerivation)
    {
        var salt = Pkdf2Utils.GetSaltBytes(value.Length);
        var result = this.DeriveValue(value, salt, hashDerivation);

        return BinaryManager.From(Pkdf2Utils.Write(result, salt)).ToString(BinaryView.Base64);
    }

    public bool Verify(string derived, string value, Pbkdf2HashDerivation hashDerivation)
    {

        var info = Pkdf2Utils.Read(BinaryManager.From(derived, BinaryView.Base64).Binary, numBytes);
        var result = this.DeriveValue(value, info.Salt.ToArray(), hashDerivation);

        return BinaryManager.From(info.Derivated.ToArray()).ToString(BinaryView.Base64) == BinaryManager.From(result).ToString(BinaryView.Base64);
    }
}
