using Application.Library.Utilities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Reflection;

namespace Application.Library.Security
{
    public static class SecurityModels
    {
        public enum HashType
        {
            SHA512 = 1,
            SHA384 = 2,
            SHA256 = 3
        }

        public enum Derivation
        {
            HMACSHA1 = 1,
            HMACSHA256 = 2,
            HMACSHA512 = 3
        }

        public static KeyDerivationPrf GetDerivationPrf(Derivation derivation) => derivation switch
        {
            Derivation.HMACSHA1 => KeyDerivationPrf.HMACSHA1,
            Derivation.HMACSHA256 => KeyDerivationPrf.HMACSHA256,
            Derivation.HMACSHA512 => KeyDerivationPrf.HMACSHA512,
            _ => throw new Exception("INVALID ARGUMENT KEY DERIVATION")
        };

        public class PbkdfInfo
        {
            private int numBytes;
            private byte[] binary;

            public PbkdfInfo(byte[] binary, int numBytes)
            {
                this.numBytes = numBytes;
                this.binary = binary;
            }

            public byte[] GetValue()
            {
                return this.binary.Skip(0).Take(this.numBytes).ToArray();
            }

            public byte[] GetSalt()
            {
                return this.binary.Skip(this.numBytes).ToArray();
            }
        }

        public class SecuritySaltReaded
        {
            public byte[] derived { get; set; } = new byte[0];
            public byte[] salt { get; set; } = new byte[0];
        }

        public class SecuritySaltWrited
        {
            public List<byte[]> parts = new List<byte[]>();
            public byte[] salt = new byte[] { };
        } 

        public class SecuritySalt
        {
            public int size { get; set; } = 0;
            public List<byte[]> parts = new List<byte[]>();
            public byte[] salt = new byte[] { };

            public static SecuritySaltWrited Write(byte[] salt)
            {
                var model = new SecuritySaltWrited
                {
                    salt = salt
                };
                int size = salt.Length / 4;

                for (int x = 0; x < 4; x++)
                {
                    List<byte> bytes = new List<byte>();
                    for (int y = (x * size); y < (size * (x + 1)); y++)
                        bytes.Add(salt[y]);
                    model.parts.Add(bytes.ToArray());
                }

                List<byte> binarys = new List<byte>();
                for (int x = 0; x < 4; x++)
                {
                    var bytes = model.parts[x];
                    for (int y = 0; y < bytes.Length; y++)
                        binarys.Add(bytes[y]);
                }

                return model;                
            }

            public static SecuritySaltReaded Read(byte[] bytes, int size)
            {
                int saltLen = (bytes.Length - size) / 4;
                int keyLen = size / 4;
                int totalLen = saltLen + keyLen;
                List<byte[]> parts = new List<byte[]>();
                List<byte> derivedBytes = new List<byte>();
                List<byte> salthBytes = new List<byte>();

                for (int x = 0; x < 4; x++)
                {
                    List<byte> part = new List<byte>();
                    for (int y = (totalLen * x); y < (totalLen * (x + 1)); y++)
                    {
                        part.Add(bytes[y]);
                    }
                    parts.Add(part.ToArray());
                }

                List<byte> binarys = new List<byte>();
                for (int x = 0; x < 4; x++)
                {
                    binarys.AddRange(parts[x]);
                }

                for (int x = 0; x < 4; x++)
                {
                    byte[] part = parts[x];
                    for (int y = 0; y < keyLen; y++) derivedBytes.Add(part[y]);
                }

                for (int x = 0; x < 4; x++)
                {
                    byte[] part = parts[x];
                    for (int y = keyLen; y < totalLen; y++) salthBytes.Add(part[y]);
                }

                return new SecuritySaltReaded { derived = derivedBytes.ToArray(), salt = salthBytes.ToArray() };
            }

            public static byte[] UnionBytes(byte[] derived, List<byte[]> parts)
            {
                List<byte[]> partsDerived = new List<byte[]>();
                List<byte> unionParts = new List<byte>();
                int dLength = derived.Length / 4;

                for (int x = 0; x < 4; x++)
                {
                    List<byte> bytes = new List<byte>();
                    for (int y = (x * dLength); y < (dLength * (x + 1)); y++)
                    {
                        bytes.Add(derived[y]);
                    }
                    partsDerived.Add(bytes.ToArray());
                }

                for (int x = 0; x < 4; x++)
                {
                    List<byte> bytes = new List<byte>(partsDerived[x]);
                    bytes.AddRange(parts[x]);
                    unionParts.AddRange(bytes.ToArray());
                }

                return unionParts.ToArray();
            }
        }
    }
}
