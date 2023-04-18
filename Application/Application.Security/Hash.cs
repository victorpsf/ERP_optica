using Application.Interfaces.Security;
using Application.Library;
using System.Security.Cryptography;
using static Application.Models.Security.HashModels;

namespace Application.Security;

public class Hash: IHash
{
    private readonly AppHashAlgorithm cipher = AppHashAlgorithm.SHA512;

    public Hash() { }

    private Hash(AppHashAlgorithm cipher): this()
    { this.cipher = cipher; }

    public IHash Create(AppHashAlgorithm cipher) => new Hash(cipher);

    private byte[] Encipher(HashAlgorithm alg, byte[] value) => alg.ComputeHash(value);

    private HashAlgorithm _SHA512() => SHA512.Create();
    private HashAlgorithm _SHA384() => SHA384.Create();
    private HashAlgorithm _SHA256() => SHA256.Create();

    public byte[] Update(byte[] value)
    {
        HashAlgorithm alg;

        switch (this.cipher)
        {
            case AppHashAlgorithm.SHA512: alg = this._SHA512(); break;
            case AppHashAlgorithm.SHA384: alg = this._SHA384(); break;
            case AppHashAlgorithm.SHA256: alg = this._SHA256(); break;
            default:
                throw new ArgumentException("CIPHER IS NOT SUPPORTED");
        };

        return this.Encipher(alg, value);
    }

    public byte[] Update(string value) => 
        this.Update(
            BinaryConverter.ToBytesView(
                value, 
                Models.Security.BinaryViewModels.BinaryView.BINARY
            )
        );
}
