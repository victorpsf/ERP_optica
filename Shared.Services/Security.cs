using Application.Library.Utilities;
using System.Security.Cryptography;
using static Application.Library.Utilities.CipherResult;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySqlX.XDevAPI.Relational;
using static Application.Library.Security.SecurityModels;

namespace Shared.Services
{
    public class Security
    {
        public static Hash CreateHash(HashType cipher) => new Hash(cipher);

        public static string Pbkdf2(string value, Derivation derivation)
            => AppKeyDerivation.Create().Pbkdf2(value, derivation);

        /// <summary>
        ///     verifica integridade da senha fornecida com a senha salva
        /// </summary>
        /// <param name="v1">cipher value</param>
        /// <param name="v2">value</param>
        /// <returns></returns>
        public static bool Pbkdf2Verify(string v1, string v2, Derivation derivation) =>
            AppKeyDerivation.Create().Pbkdf2Verify(v1, v2, derivation);
    }
}
