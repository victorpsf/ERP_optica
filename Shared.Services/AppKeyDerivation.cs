using Application.Library.Security;
using Application.Library.Utilities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Library.Security.SecurityModels;
using static Application.Library.Utilities.CipherResult;

namespace Shared.Services
{
    public sealed class AppKeyDerivation
    {
        private int numBytes = 8192;

         public string Pbkdf2(string value, Derivation derivation)
        {
            var securitySalt = BinaryUtil.GetSaltBytes(value);

            var result = new BinaryResult(
                KeyDerivation.Pbkdf2(
                    password: value,
                    salt: securitySalt.salt,
                    prf: SecurityModels.GetDerivationPrf(derivation),
                    iterationCount: value.Length * 10,
                    numBytesRequested: this.numBytes
                )
            );

            return BinaryConverter.ToStringView(SecuritySalt.UnionBytes(result.Binary, securitySalt.parts), BinaryConverter.StringView.BASE64);
        }

        public bool Pbkdf2Verify(string v1, string v2, Derivation derivation)
        {
            var readed = SecuritySalt.Read(BinaryConverter.ToBytesView(v1, BinaryConverter.StringView.BASE64), this.numBytes);

            var result = new BinaryResult(
                KeyDerivation.Pbkdf2(
                    password: v2,
                    salt: readed.salt,
                    prf: SecurityModels.GetDerivationPrf(derivation),
                    iterationCount: v2.Length * 10,
                    numBytesRequested: this.numBytes
                )
            );

            return BinaryConverter.ToStringView(result.Binary, BinaryConverter.StringView.BASE64) == BinaryConverter.ToStringView(readed.derived, BinaryConverter.StringView.BASE64);
        }

        public static AppKeyDerivation Create()
        { return new AppKeyDerivation(); }
    }
}
