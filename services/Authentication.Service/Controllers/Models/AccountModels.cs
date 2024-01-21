using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Controllers.Models;

public static class AccountModels
{
    public class SingInInput
    {
        [Required(ErrorMessage = "ERROR_SING_IN_NAME_IS_NOT_INFORMED")]
        [MaxLength(255, ErrorMessage = "ERROR_SING_IN_NAME_OUT_OF_RANGE")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "ERROR_SING_IN_PASSWORD_IS_NOT_INFORMED")]
        [MaxLength(350, ErrorMessage = "ERROR_SING_IN_PASSWORD_OUT_OF_RANGE")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "ERROR_SING_IN_ENTERPRISEID_IS_NOT_INFORMED")]
        public int? EnterpriseId { get; set; }
    }

    public class ValidateCode
    {
        [Required(ErrorMessage = "ERROR_SING_IN_CODE_IS_NOT_INFORMED")]
        [MaxLength(9, ErrorMessage = "ERROR_SING_IN_CODE_OUT_OF_RANGE")]
        public string? Code { get; set; }
    }

    public class ResendCodeInput
    { }

    public class SingInOutput
    {
        public bool CodeSended { get; set; } 
    }

    public class ValidateCodeOutput
    {
        public DateTime? Expire { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public class SendedOutput
    {
        public DateTime sended { get; set; }
    }

    public class ForgottenInput
    {
        [MaxLength(255, ErrorMessage = "ERROR_FORGOTTEN_NAME_OUT_OF_RANGE")]
        public string? Name { get; set; }

        [MaxLength(255, ErrorMessage = "ERROR_FORGOTTEN_EMAIL_OUT_OF_RANGE")]
        public string? Email { get; set; }
    }

    public class ForgottenOutput
    {
        public bool CodeSended { get; set; }
    }

    public class ForgottenValidateCodeInput
    {
        [Required(ErrorMessage = "ERROR_FORGOTTEN_CODE_IS_NOT_INFORMED")]
        [MaxLength(9, ErrorMessage = "ERROR_FORGOTTEN_CODE_OUT_OF_RANGE")]
        public string? Code { get; set; }
    }

    public class ForgottenValidateCodeOutput
    {
        public bool Success { get; set; }
    }

    public class ForgottenChangePassphraseInput
    {
        [Required(ErrorMessage = "ERROR_FORGOTTEN_PASSPHRASE_IS_NOT_INFORMED")]
        [MinLength(8, ErrorMessage = "ERROR_FORGOTTEN_PASSPHRASE_OUT_OF_RANGE")]
        public string? Passphrase { get; set; }

        [Required(ErrorMessage = "ERROR_FORGOTTEN_PASSPHRASE_IS_NOT_INFORMED")]
        [MinLength(8, ErrorMessage = "ERROR_FORGOTTEN_PASSPHRASE_OUT_OF_RANGE")]
        public string? Confirm { get; set; }
    }

    public class ForgottenChangePassphraseOutput
    {
        public bool Success { get; set; }
    }
}
