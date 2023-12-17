using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Services;
using Authentication.Service.Repositories.Rules;
using Authentication.Service.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Authentication.Service.Controllers.Models.AccountModels;

namespace Authentication.Service.Controllers;

public partial class ForgottenController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;
    private readonly AuthenticateRepoService service;

    public ForgottenController(
        IBaseControllerServices baseControllerServices,
        AuthenticateRepoService service
    )
    {
        this.baseControllerServices = baseControllerServices;
        this.service = service;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Index([FromBody] ForgottenInput input)
    {
        var output = new ControllerBaseModels.RequestResult<ForgottenOutput>();

        try
        {
            if (this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            if (string.IsNullOrEmpty(input.Name) && string.IsNullOrEmpty(input.Email))
            {
                output.addError(this.baseControllerServices.getMessage(MultiLanguageModels.MessagesEnum.ERROR_FORGOTTEN_NAME_IS_NOT_INFORMED), "Name");
                output.addError(this.baseControllerServices.getMessage(MultiLanguageModels.MessagesEnum.ERROR_FORGOTTEN_EMAIL_IS_NOT_INFORMED), "E-mail");
                throw new ControllerEmptyException();
            }

            var info = this.service.Find(new AuthenticateRules.ForgottenRule
            {
                Name = string.IsNullOrEmpty(input.Name)? null: this.baseControllerServices.NewHash(SecurityModels.AppHashAlgorithm.SHA512)
                    .Update(input.Name, BinaryManagerModels.BinaryView.Base64),
                Email = input.Email
            });

            if (info is null)
            {
                output.addResult(
                    new ForgottenOutput { CodeSended = true }
                );
                throw new ControllerEmptyException();
            }

            var rule = new AuthenticateRules.CodeRule { AuthId = info.UserId, CodeType = AccountDtos.CodeTypeEnum.FORGOTTEN.intValue() };
            var code = this.service.Find(rule);

            if (code is null)
                this.SendCodeEmail(this.service.Create(rule), info);

            this.baseControllerServices.hostCache.Set("try:forgotten", info, 240);
            output.addResult(
                new ForgottenOutput { CodeSended = true }
            );
        }

        catch (ControllerEmptyException) { }
        catch (BusinessException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (AppDbException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (Exception ex)
        {
            this.baseControllerServices.logger.PrintsTackTrace(ex);
            output.addError(this.baseControllerServices.getMessage(null), null);
        }

        return output.Failed ? BadRequest(output): Ok(output);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ValidateCode([FromBody] ForgottenValidateCodeInput input)
    {
        var output = new ControllerBaseModels.RequestResult<ForgottenValidateCodeOutput>();

        try
        {
            if (this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            var cache = this.baseControllerServices.hostCache.Get<AccountDtos.ForgottenDto>("try:forgotten");

            if (cache is null)
            {
                output.Result = new ForgottenValidateCodeOutput
                {
                    Sucess = false
                };
                throw new ControllerEmptyException();
            }

            var rule = new AuthenticateRules.CodeRule { AuthId = cache.UserId, CodeType = AccountDtos.CodeTypeEnum.FORGOTTEN.intValue() };
            var code = this.service.Find(rule);

            if (code is null || input.Code != code.Code)
            {
                output.Result = new ForgottenValidateCodeOutput
                {
                    Sucess = false
                };
                throw new ControllerEmptyException();
            }

            this.baseControllerServices.hostCache.Set("try:forgotten", cache, 240);
            this.baseControllerServices.hostCache.Set("try:forgotten:code", code, 240);
            output.Result = new ForgottenValidateCodeOutput
            {
                Sucess = true
            };
        }

        catch (ControllerEmptyException) { }
        catch (BusinessException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (AppDbException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (Exception ex)
        {
            this.baseControllerServices.logger.PrintsTackTrace(ex);
            output.addError(this.baseControllerServices.getMessage(null), null);
        }

        return output.Failed ? BadRequest(output) : Ok(output);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ChangePassword([FromBody] ForgottenChangePassphraseInput input)
    {
        var output = new ControllerBaseModels.RequestResult<ForgottenValidateCodeOutput>();

        try
        {
            if (this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            var user = this.baseControllerServices.hostCache.Get<AccountDtos.ForgottenDto>("try:forgotten");
            var code = this.baseControllerServices.hostCache.Get<AccountDtos.CodeDto>("try:forgotten:code");

            if (user is null || code is null || input.Passphrase != input.Confirm)
            {
                output.Result = new ForgottenValidateCodeOutput
                {
                    Sucess = false
                };
                throw new ControllerEmptyException();
            }

            this.service.Save(new AuthenticateRules.ForgottenChangePassphraseRule
            {
                AuthId = user.UserId,
                Passphrase = this.baseControllerServices.NewPbkdf2()
                    .Write(
                        this.baseControllerServices.NewHash(SecurityModels.AppHashAlgorithm.SHA512)
                            .Update(input.Passphrase ?? string.Empty, BinaryManagerModels.BinaryView.Base64), 
                        SecurityModels.Pbkdf2HashDerivation.HMACSHA512
                     )
            });

            var rule = new AuthenticateRules.CodeRule { AuthId = user.UserId, CodeType = AccountDtos.CodeTypeEnum.FORGOTTEN.intValue() };
            this.service.Delete(rule);
            this.baseControllerServices.hostCache.Unset("try:forgotten");
            this.baseControllerServices.hostCache.Unset("try:forgotten:code");
            output.Result = new ForgottenValidateCodeOutput { Sucess = true };
        }

        catch (ControllerEmptyException) { }
        catch (BusinessException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (AppDbException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (Exception ex)
        {
            this.baseControllerServices.logger.PrintsTackTrace(ex);
            output.addError(this.baseControllerServices.getMessage(null), null);
        }

        return output.Failed ? BadRequest(output) : Ok(output);
    }
}
