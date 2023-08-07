﻿namespace Authentication.Service.Repositories.Rules;

public static class AuthenticateRules
{
    public class SingInRule
    {
        public string Login { get; set; } = string.Empty;
        public int EnterpriseId { get; set; }
    }
}