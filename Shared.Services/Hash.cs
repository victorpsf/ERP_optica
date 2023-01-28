using Application.Library.Utilities;
using System.Security.Cryptography;
using static Application.Library.Security.SecurityModels;
using static Application.Library.Utilities.CipherResult;

namespace Shared.Services
{
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
}
