using System.Reflection;
using static Application.Models.Controller.ValidationModels;
using static Application.Models.MultiLanguageModels;

namespace Application.Models.Controller;

public static class ValidationModels
{
    public class AppError
    {
        public string Attribute { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class AppValidationResult
    {
        public bool Failed { get; set; }
        public List<AppError> Erros = new List<AppError>();

        public AppValidationResult()
        { }

        public AppValidationResult(List<AppError> erros)
        {
            this.Failed = erros.Any();
            this.Erros = erros;
        }
    }

    public enum AppValidateRuleEnum
    {
        Required,
        Min,
        Max
    }

    public class AppValidateRule
    {
        public AppValidateRuleEnum Rule { get; set; }
        public MessagesEnum Stack { get; set; } = string.Empty;
        public int? value { get; set; }
    }

    public class AppValidate
    {
        public string Attribute { get; set; } = string.Empty;
        public List<AppValidateRule> Rule { get; set; } = new List<AppValidateRule>();
    }

    public class AppValidationInfo
    {
        public PropertyInfo? Info { get; set; }
        public string Attribute { get; set; } = string.Empty;
        public object? Value { get; set; }
    }
}
