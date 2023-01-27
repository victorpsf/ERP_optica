using Application.Library.Utilities;
using System.Security.Cryptography;
using static Application.Library.Utilities.CipherResult;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySqlX.XDevAPI.Relational;

namespace Shared.Services
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

    public class PbkdfInfo {
        private int numBytes;
        private byte[] binary;

        public PbkdfInfo(byte[] binary, int numBytes) {
            this.numBytes = numBytes;
            this.binary = binary;
        }

        public byte[] GetValue() {
            return this.binary.Skip(0).Take(this.numBytes).ToArray();
        }

        public byte[] GetSalt() {
            return this.binary.Skip(this.numBytes).ToArray();
        }
    }

    public class Hash
    {
        private readonly HashType cipher = HashType.SHA512;
        private byte[] binary = new byte[] { };
        private HashAlgorithm alg = SHA512.Create();

        public Hash(HashType cipher)
        { this.cipher = cipher; }


        private BinaryResult Encipher() => new BinaryResult(this.alg.ComputeHash(binary));
        private void _SHA512() => this.alg = SHA512.Create();
        private void _SHA384() => this.alg = SHA384.Create();
        private void _SHA256() => this.alg = SHA256.Create();

        public BinaryResult Update(byte[] value)
        {
            this.binary = value;

            switch (this.cipher)
            {
                case HashType.SHA512: this._SHA512(); break;
                case HashType.SHA384: this._SHA384(); break;
                case HashType.SHA256: this._SHA256(); break;
                default:
                    throw new ArgumentException("CIPHER IS NOT SUPPORTED");
            };

            return this.Encipher();
        }

        public BinaryResult Update(string value) => this.Update(BinaryConverter.ToBytesView(value, BinaryConverter.StringView.BINARY));
    }

    public class Security
    {
        private static int numBytes = 8192;

        private static byte[] RandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            var r = new Random();
            r.NextBytes(bytes);
            return bytes;
        }

        private static KeyDerivationPrf GetDerivationPrf(Derivation derivation) => derivation switch
        {
            Derivation.HMACSHA1 => KeyDerivationPrf.HMACSHA1,
            Derivation.HMACSHA256 => KeyDerivationPrf.HMACSHA256,
            Derivation.HMACSHA512 => KeyDerivationPrf.HMACSHA512,
            _ => throw new Exception("INVALID ARGUMENT KEY DERIVATION")
        };

        public static Hash CreateHash(HashType cipher) => new Hash(cipher);
        public static string Pbkdf2(string value, Derivation derivation)
        {
            byte[] sBytes = RandomBytes(value.Length);

            var result = new BinaryResult(
                KeyDerivation.Pbkdf2(
                    password: value, 
                    salt: sBytes, 
                    prf: GetDerivationPrf(derivation),
                    iterationCount: value.Length, 
                    numBytesRequested: numBytes
                )
            );

            var binaryList = new List<byte>(result.Binary);
            binaryList.AddRange(sBytes);
            return BinaryConverter.ToStringView(binaryList.ToArray(), BinaryConverter.StringView.BASE64);
        }

        /// <summary>
        ///     verifica integridade da senha fornecida com a senha salva
        /// </summary>
        /// <param name="v1">cipher value</param>
        /// <param name="v2">value</param>
        /// <returns></returns>
        public static bool Pbkdf2Verify(string v1, string v2, Derivation derivation)
        {
            var info = new PbkdfInfo(BinaryConverter.ToBytesView(v1, BinaryConverter.StringView.BASE64), numBytes);

            var result = new BinaryResult(
                KeyDerivation.Pbkdf2(
                    password: v2,
                    salt: info.GetSalt(),
                    prf: GetDerivationPrf(derivation),
                    iterationCount: v2.Length,
                    numBytesRequested: numBytes
                )
            );

            return BinaryConverter.ToStringView(result.Binary, BinaryConverter.StringView.BASE64) == BinaryConverter.ToStringView(info.GetValue(), BinaryConverter.StringView.BASE64);
        }
    }
}
