using static Application.Models.Controller.ValidationModels;
using static Application.Models.MultiLanguageModels;

namespace Application.Interfaces;

public interface IModelValidation
{
    AppValidateRule GetRule(AppValidateRuleEnum rule, MessagesEnum stack, int? value);
    AppValidateRule GetRule(AppValidateRuleEnum rule, MessagesEnum stack);
    AppValidate GetValidationRule(string attribute, params AppValidateRule[] rules);
    AppValidationResult Validate<T>(T model, IAppControllerServices services, params AppValidate[] rules) where T : class;
}
