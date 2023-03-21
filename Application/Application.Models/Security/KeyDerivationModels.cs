namespace Application.Models.Security;

public static class KeyDerivationModels
{
    public enum HashDerivation
    {
        HMACSHA1 = 1,
        HMACSHA256 = 2,
        HMACSHA512 = 3
    }

    public class ValueInfo
    {
        public byte[] Value { get; set; } = new byte[] { };
        public List<byte[]> Parts { get; set; } = new List<byte[]>();
    }

    public class ValueSizes
    {
        public int DerivedSize { get; set; }
        public int SaltSize { get; set; }
    }

    public class DerivationInfo
    {
        public List<byte> Derivated { get; set; } = new List<byte> { };
        public List<byte> Salt { get; set; } = new List<byte> { };
    }
}
