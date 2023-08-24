namespace Authentication.Service.Repositories.Rules;

public static class AuthenticateRules
{
    public class SingInRule
    {
        public string Login { get; set; } = string.Empty;
        public int EnterpriseId { get; set; }
    }

    public class CodeRule
    {
        public int AuthId { get; set; }
        public int CodeType { get; set; }
    }

    public class ResendCodeRule
    {
        public string Login { get; set; } = string.Empty;
        public int EnterpriseId { get; set; }
        public int CodeType { get; set; }
    }

    public class EnterpriseRule
    {

    }
}
