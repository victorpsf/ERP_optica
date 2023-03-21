using Application.Library;
using System.Security.Cryptography;
using static Application.Models.Security.ChipherModels;
using static Application.Models.Security.HashModels;

namespace Application.Security;

public class Hash
{
    private readonly AppHashAlgorithm cipher = AppHashAlgorithm.SHA512;
    private byte[] binary = new byte[] { };
    private HashAlgorithm alg = SHA512.Create();

    private Hash(AppHashAlgorithm cipher)
    { this.cipher = cipher; }

    public static Hash Create(AppHashAlgorithm cipher) => new Hash(cipher);

    private byte[] Encipher() => this.alg.ComputeHash(binary);
    private void _SHA512() => this.alg = SHA512.Create();
    private void _SHA384() => this.alg = SHA384.Create();
    private void _SHA256() => this.alg = SHA256.Create();

    public byte[] Update(byte[] value)
    {
        this.binary = value;

        switch (this.cipher)
        {
            case AppHashAlgorithm.SHA512: this._SHA512(); break;
            case AppHashAlgorithm.SHA384: this._SHA384(); break;
            case AppHashAlgorithm.SHA256: this._SHA256(); break;
            default:
                throw new ArgumentException("CIPHER IS NOT SUPPORTED");
        };

        return this.Encipher();
    }

    public byte[] Update(string value) => this.Update(BinaryConverter.ToBytesView(value, Models.Security.BinaryViewModels.BinaryView.BINARY));
}
