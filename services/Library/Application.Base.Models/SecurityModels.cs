namespace Application.Base.Models;

public static class SecurityModels
{
    public enum AppHashAlgorithm
    {
        SHA512 = 1,
        SHA384 = 2,
        SHA256 = 3
    }

    public enum Pbkdf2HashDerivation
    {
        HMACSHA1 = 1,
        HMACSHA256 = 2,
        HMACSHA512 = 3
    }

    public class Pbkdf2ValueInfo
    {
        public byte[] Value { get; set; } = new byte[] { };
        public List<byte[]> Parts { get; set; } = new List<byte[]>();
    }

    public class Pbkdf2ValueSizes
    {
        public int DerivedSize { get; set; }
        public int SaltSize { get; set; }
    }

    public class Pbkdf2DerivationInfo
    {
        public List<byte> Derivated { get; set; } = new List<byte> { };
        public List<byte> Salt { get; set; } = new List<byte> { };
    }
}
