namespace Application.Controller.Models;

public static class AccountModels
{
    public enum AuthenticationSteps
    {
        None = 0,
        CODE_SENDED = 1,
        AUTHENTICATED = 2
    }

    public class SingInInput
    {
        public string? Name { get; set; }
        public string? Key { get; set; }
        public string? Code { get; set; }
        public int? EnterpriseId { get; set; }

        public int GetCode()
        {
            try
            { return Convert.ToInt32(this.Code); }
            catch
            { throw new Exception("USER_AND_KEY_AND_CODE_INVALID"); }
        }
    }

    public class ResendEmailInput
    {
        public string Name { get; set; } = string.Empty;
        public int EnterpriseId { get; set; }
    }

    public class SingInOutput
    {
        public AuthenticationSteps Step { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public string? Token { get; set; } = string.Empty;
    }
}
