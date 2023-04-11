using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using static Application.Models.Controller.ValidationModels;
using static Application.Models.MultiLanguageModels;

namespace Application.Library;

public class ModelValidation
{
    protected readonly IConfiguration configuration;

    public ModelValidation(IConfiguration configuration)
    { this.configuration = configuration; }

    public AppValidateRule GetRule(AppValidateRuleEnum rule, MessagesEnum stack, int? value) 
        => new AppValidateRule { 
            Rule = rule, 
            Stack = stack, 
            value = value 
        };

    public AppValidate GetValidationRule(string attribute, params AppValidateRule[] rules)
        => new AppValidate
        {
            Attribute = attribute,
            Rule = new List<AppValidateRule>(rules)
        };


    public AppValidationResult Validate<T>(T model, IMessage message, params AppValidate[] rules) where T : class
         => ModelValidator<T>.GetInstance(
             model,
             message,
             new List<AppValidate>(rules)
         ).Validate();
}
