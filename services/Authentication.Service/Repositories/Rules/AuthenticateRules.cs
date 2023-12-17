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

    public class ForgottenRule
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    public class ForgottenChangePassphraseRule
    {
        public int AuthId { get; set; }
        public string Passphrase { get; set; } = string.Empty;
    }
}
