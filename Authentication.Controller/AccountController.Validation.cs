using Authentication.Controller.Models;
using static Application.Library.ControllerModels;
using Shared.Services;

namespace Authentication.Controller
{
    public partial class AccountController
    {
        private void Validate(AccountModels.SingInInput input, List<Failure> Errors)
        {
            if (input is null)
            {
                Errors.Add(new Failure { Message = "Formato de valores não informado" });
                throw new Exception("VALIDATION_FAILURE");
            }

            if (string.IsNullOrEmpty(input.Name)) Errors.Add(new Failure { Field = "Name", Message = "Usuário não informado" });
            else
            {
                if (!(input.Name.Length > 5)) Errors.Add(new Failure { Field = "Name", Message = "Nome do usuário muito curto" });
            }

            if (string.IsNullOrEmpty(input.Key)) Errors.Add(new Failure { Field = "Key", Message = "Senha não informada" });
            else
            {
                if (!(input.Key.Length > 12)) Errors.Add(new Failure { Field = "Key", Message = "Senha muito curta" });
            }

            if (!(input.EnterpriseId > 0)) Errors.Add(new Failure { Field = "EnterpriseId", Message = "Empresa não informada" });

            if (Errors.Any()) throw new Exception("VALIDATION_FAILURE");
        }

        private bool Validate(String Code) {
            if (string.IsNullOrEmpty(Code)) return false;

            try {
                int codeInterger = Convert.ToInt32(Code);
                return (codeInterger > 100000 && codeInterger < 999999);
            } 
            
            catch 
            { return false; }
        }

        private bool Validate(int informedCode, int code) {
            return informedCode == code;
        }

        private bool Validate(string userKey, string informedKey) {
            return Security.Pbkdf2Verify(userKey, informedKey, Derivation.HMACSHA512);
        }

        private void Validate(AccountModels.ResendEmailInput input, List<Failure> Errors)
        {
            if (input is null)
            {
                Errors.Add(new Failure { Message = "Formato de valores não informado" });
                throw new Exception("VALIDATION_FAILURE");
            }

            if (string.IsNullOrEmpty(input.Name)) Errors.Add(new Failure { Field = "Name", Message = "Usuário não informado" });
            else
            {
                if (!(input.Name.Length > 5)) Errors.Add(new Failure { Field = "Name", Message = "Nome do usuário muito curto" });
            }

            if (!(input.EnterpriseId > 0)) Errors.Add(new Failure { Field = "EnterpriseId", Message = "Empresa não informada" });
        }
    }
}
