using Application.Controller.Models;
using static Application.Models.Controller.ValidationModels;
using static Application.Models.MultiLanguageModels;
using Application.Exceptions;

namespace Service.Authentication.Controllers;

public partial class AccountController
{
    private void Validate(AccountModels.SingInInput input)
    {
        var result = this.services.validation.Validate(
            input, 
            this.services,
            this.services.validation.GetValidationRule(
                "Name",
                this.services.validation.GetRule(
                    AppValidateRuleEnum.Required,
                    MessagesEnum.ERRO_LOGIN_NAME_REQUIRED
                )
            ),
            this.services.validation.GetValidationRule(
                "Key",
                this.services.validation.GetRule(
                    AppValidateRuleEnum.Required,
                    MessagesEnum.ERRO_LOGIN_KEY_REQUIRED
                )
            ),
            this.services.validation.GetValidationRule(
                "EnterpriseId",
                this.services.validation.GetRule(
                    AppValidateRuleEnum.Required,
                    MessagesEnum.ERRO_LOGIN_ENTERPRISEID_REQUIRED
                )
            )
        );


        if (result.Failed) throw new AppValidationException(result.Erros);
    }
}
