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

        [MaxLength(9, ErrorMessage = "ERROR_SING_IN_CODE_OUT_OF_RANGE")]
        public string? Code { get; set; }
    }

    public class ResendCodeInput
    {
        [Required(ErrorMessage = "ERROR_SING_IN_NAME_IS_NOT_INFORMED")]
        [MaxLength(255, ErrorMessage = "ERROR_SING_IN_NAME_OUT_OF_RANGE")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "ERROR_SING_IN_ENTERPRISEID_IS_NOT_INFORMED")]
        public int? EnterpriseId { get; set; }
    }

    public class SingInOutput
    {
        public DateTime? Expire { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool CodeSended { get; set; } 
    }

    public class SendedOutput
    {
        public DateTime sended { get; set; }
    }
}
