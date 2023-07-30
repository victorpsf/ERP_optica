using Application.Interfaces.Security;
using Application.Utils;
using System.Security.Cryptography;
using static Application.Base.Models.BinaryManagerModels;
using static Application.Base.Models.SecurityModels;

namespace Application.Security;

public class Hash: IHash
{
    private readonly AppHashAlgorithm cipher = AppHashAlgorithm.SHA512;

    private Hash(AppHashAlgorithm cipher)
    { this.cipher = cipher; }

    private byte[] Encipher(HashAlgorithm alg, byte[] value) => alg.ComputeHash(value);

    private HashAlgorithm _SHA512() => SHA512.Create();
    private HashAlgorithm _SHA384() => SHA384.Create();
    private HashAlgorithm _SHA256() => SHA256.Create();

    private byte[] Update(byte[] value)
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

    public string Update(string value, BinaryView outputFormat) =>
        BinaryManager.From(
            this.Update(
                // convert string to binary
                BinaryManager.ToByteArray(
                    value, 
                    BinaryView.String // binary string format
                )
            )
        )
        .ToString(outputFormat); // transform to format output [DATAVIEW]



    public static IHash Create(AppHashAlgorithm cipher) => new Hash(cipher);
}
