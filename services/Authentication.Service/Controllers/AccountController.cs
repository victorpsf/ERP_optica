using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces.Services;
using Authentication.Service.Controllers.Models;
using Authentication.Service.Repositories.Rules;
using Authentication.Service.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Extensions;
using System.Data;
using static Application.Base.Models.JwtModels;

namespace Authentication.Service.Controllers;

[AllowAnonymous]
public partial class AccountController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;
    private readonly AuthenticateRepoService service;

    public AccountController(IBaseControllerServices baseControllerServices, AuthenticateRepoService service)
    {
        this.baseControllerServices = baseControllerServices;
        this.service = service;
    }

    private AccountResultModels.ValidateInputResult ValidateInput<T> (AccountModels.SingInInput data, ControllerBaseModels.RequestResult<T> output)
    {
        var user = this.service.Find(new AuthenticateRules.SingInRule { Login = data.Name ?? string.Empty, EnterpriseId = data.EnterpriseId ?? default });

        if (user is null)
        {
            output.addError(this.baseControllerServices.getMessage(MultiLanguageModels.MessagesEnum.ERROR_USER_DONT_FOUND), "Name");
            throw new ControllerEmptyException();
        }

        if (!this.baseControllerServices.NewPbkdf2().Verify(user.Password, data.Password ?? string.Empty, SecurityModels.Pbkdf2HashDerivation.HMACSHA512))
        {
            output.addError(this.baseControllerServices.getMessage(MultiLanguageModels.MessagesEnum.ERROR_PASSWORD_INCORRECT), "Name");
            throw new ControllerEmptyException();
        }

        var rule = new AuthenticateRules.CodeRule { AuthId = user.UserId, CodeType = AccountDtos.CodeTypeEnum.AUTHENTICATION.intValue() };
        var code = this.service.Find(rule);

        return new AccountResultModels.ValidateInputResult
        {
            User = user,
            Code = code,
            Rule = rule
        };
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SingIn([FromBody] AccountModels.SingInInput data)
    {
        var output = new ControllerBaseModels.RequestResult<AccountModels.SingInOutput>();

        try
        {
            if (!this.baseControllerServices.validator.validate(data, output))
                throw new ControllerEmptyException();

            data.Name = this.baseControllerServices.NewHash(SecurityModels.AppHashAlgorithm.SHA512)
                .Update(data.Name ?? string.Empty, BinaryManagerModels.BinaryView.Base64);
            data.Password = this.baseControllerServices.NewHash(SecurityModels.AppHashAlgorithm.SHA512)
                .Update(data.Password ?? string.Empty, BinaryManagerModels.BinaryView.Base64);

            var result = this.ValidateInput(data, output);
            this.baseControllerServices.hostCache.Set("try:login", data, 240);

            if (result.Code is null)
            {
                this.SendCodeEmail(this.service.Create(result.Rule), result.User);
                return Ok(output.addResult(new AccountModels.SingInOutput { CodeSended = true }));
            }

            else return Ok(output.addResult(new AccountModels.SingInOutput { CodeSended = true }));
        }

        catch(ControllerEmptyException)
        { }

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
    public IActionResult ValidateCode([FromBody] AccountModels.ValidateCode data)
    {
        var output = new ControllerBaseModels.RequestResult<AccountModels.ValidateCodeOutput>();

        try
        {
            if (!this.baseControllerServices.validator.validate(data, output))
                throw new ControllerEmptyException();

            var cache = this.baseControllerServices.hostCache.Get<AccountModels.SingInInput>("try:login");

            if (cache is null) {
                output.addError(this.baseControllerServices.getMessage(MultiLanguageModels.MessagesEnum.ERROR_EMPTY_SINGIN_CACHE), null);
                throw new ControllerEmptyException();
            }

            var result = this.ValidateInput(cache, output);

            if (result.Code is null)
            {
                throw new ControllerEmptyException();
            }

            if (data.Code != result.Code.Code || DateTime.UtcNow > result.Code.ExpireIn)
                return BadRequest(
                    output.addError(this.baseControllerServices.getMessage(MultiLanguageModels.MessagesEnum.ERROR_CODE_INVALID), "Code")
                );

            this.baseControllerServices.hostCache.Unset("try:login");
            this.service.Delete(result.Rule);
            this.SendAuthenticatedEmail(result.Code, result.User);
            this.baseControllerServices.jwtService.Write(new JwtModels.ClaimIdentifier { UserId = result.User.UserId.ToString(), EnterpriseId = result.User.EnterpriseId.ToString() }, out TokenCreated generated);
            output.addResult(new AccountModels.ValidateCodeOutput { Expire = generated.Expire, Token = generated.Token });
        }

        catch (ControllerEmptyException)
        { }

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
    public IActionResult ResendCode([FromBody] AccountModels.ResendCodeInput data)
    {
        var output = new ControllerBaseModels.RequestResult<AccountModels.SendedOutput>();

        try
        {
            var cache = this.baseControllerServices.hostCache.Get<AccountModels.SingInInput>("try:login");

            if (cache is null)
            {
                output.addError(this.baseControllerServices.getMessage(MultiLanguageModels.MessagesEnum.ERROR_EMPTY_SINGIN_CACHE), null);
                throw new ControllerEmptyException();
            }

            var result = this.ValidateInput(cache, output);

            if (!this.baseControllerServices.validator.validate(data, output))
                return BadRequest(output);

            if (result.Code is not null)
            {
                this.SendResendCodeEmail(result.Code, result.User);
                output.addResult(new AccountModels.SendedOutput { sended = DateTime.UtcNow });
            }
        }

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

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetEnterprises([FromQuery] object data)
    {
        var output = new ControllerBaseModels.RequestResult<List<AccountDtos.EnterpriseOptionDto>>();

        try
        {
            output.addResult(this.service.getEnterprises(new AuthenticateRules.EnterpriseRule { }).Select(a => new AccountDtos.EnterpriseOptionDto { Value = a.EnterpriseId, Label = a.Name }).ToList());
        }

        catch (BusinessException ex) 
        { this.baseControllerServices.logger.PrintsTackTrace(ex); }

        catch (AppDbException ex)
        { this.baseControllerServices.logger.PrintsTackTrace(ex); }

        catch (Exception ex)
        { this.baseControllerServices.logger.PrintsTackTrace(ex); }

        return Ok(output);
    }
}
