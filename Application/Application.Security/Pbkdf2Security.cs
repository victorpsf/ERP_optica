using Application.Library;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using static Application.Models.Security.KeyDerivationModels;

namespace Application.Security;

internal sealed class Pkdf2Utils
{
    private static int totalParts = 8;
    private static int minLengthSalt = 400;

    public static byte[] RandomBytes (int length)
    {
        var bytes = new byte[length];
        var random = new Random();
        random.NextBytes(bytes);
        return bytes;
    }

    public static int GetLength(int length)
    {
        decimal rest = 0;

        do
        {
            length++;
            rest = Convert.ToDecimal(length % 2);
        } while (rest != 0);

        return (length < minLengthSalt) ? GetLength(length) : length;
    }

    public static byte[] GetSaltBytes(int length) => Pkdf2Utils.RandomBytes(GetLength(length));

    public static ValueInfo GetParts(byte[] value)
    {
        List<byte[]> parts = new List<byte[]>();
        var size = value.Length / totalParts;

        for (int x = 0; x < totalParts; x++)
        {
            List<byte> bytes = new List<byte>();

            for (int y = (x * size); y < (size * (x + 1)); y++)
                bytes.Add(value[y]);

            parts.Add(bytes.ToArray());
        }

        return new ValueInfo { 
            Value = value,
            Parts = parts
        };
    }

    public static byte[] Write (byte[] derived, byte[] salt)
    {
        List<byte> values = new List<byte>();
        var derivedInfo = GetParts(derived);
        var saltInfo = GetParts(salt);

        for (int a = 0; a < totalParts; a++)
        {
            List<byte> bytes = new List<byte> ();
            bytes.AddRange(derivedInfo.Parts[a]);
            bytes.AddRange(saltInfo.Parts[a]);
            values.AddRange(bytes.ToArray ());
        }

        return values.ToArray();
    }

    public static DerivationInfo Read(byte[] value, int length)
    {
        var parts = GetParts(value);
        var size = length / totalParts;
        var info = new DerivationInfo();

        for (var a = 0; a < parts.Parts.Count; a++)
        {
            var part = parts.Parts[a];
            List<byte> derivedBytes = new List<byte>();
            List<byte> saltBytes = new List<byte>();

            for (int i = 0; i < size; i++) derivedBytes.Add(part[i]);
            for (int i = size; i < part.Length; i++) saltBytes.Add(part[i]);

            info.Derivated.AddRange(derivedBytes.ToArray());
            info.Salt.AddRange(saltBytes.ToArray());
        }

        return info;
    }
}

public sealed class Pbkdf2Security
{
    private static int numBytes = 8192;

    private static KeyDerivationPrf Convert(HashDerivation hashDerivation) => hashDerivation switch
    {
        HashDerivation.HMACSHA512 => KeyDerivationPrf.HMACSHA512,
        HashDerivation.HMACSHA256 => KeyDerivationPrf.HMACSHA256,
        HashDerivation.HMACSHA1 => KeyDerivationPrf.HMACSHA1,
        _ => throw new ArgumentException($"[ERROR HashDerivation] Pbkdf2Security: {hashDerivation.ToString()} is not defined")
    };

    private static byte[] DeriveValue(string value, byte[] salt, HashDerivation hashDerivation) => 
        KeyDerivation.Pbkdf2(password: value, salt: salt, prf: Convert(hashDerivation), iterationCount: value.Length * 10, numBytesRequested: numBytes);

    public static string Write(string value, HashDerivation hashDerivation)
    {
        var salt = Pkdf2Utils.GetSaltBytes(value.Length);
        var result = DeriveValue(value, salt, hashDerivation);

        return BinaryConverter.ToStringView(Pkdf2Utils.Write(result, salt), Models.Security.BinaryViewModels.BinaryView.BASE64);
    }

    public static bool Verify(string derived, string value,  HashDerivation hashDerivation)
    {
        var info = Pkdf2Utils.Read(BinaryConverter.ToBytesView(derived, Models.Security.BinaryViewModels.BinaryView.BASE64), numBytes);
        var result = DeriveValue(value, info.Salt.ToArray(), hashDerivation);

        return BinaryConverter.ToStringView(result, Models.Security.BinaryViewModels.BinaryView.BASE64) == BinaryConverter.ToStringView(info.Derivated.ToArray(), Models.Security.BinaryViewModels.BinaryView.BASE64);
    }
}
