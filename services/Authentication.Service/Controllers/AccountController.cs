using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces.Services;
using Authentication.Service.Controllers.Models;
using Authentication.Service.Repositories.Rules;
using Authentication.Service.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Base.Models.JwtModels;
using Application.Extensions;
using static Authentication.Service.Repositories.Rules.AuthenticateRules;
using static Application.Dtos.AccountDtos;

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

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SingIn([FromBody] AccountModels.SingInInput data)
    {
        var output = new ControllerBaseModels.RequestResult<AccountModels.SingInOutput>();

        try
        {
            if (!this.baseControllerServices.validator.validate(data, output))
                return BadRequest(output);

            var user = this.service.Find(new AuthenticateRules.SingInRule
            {
                Login = this.baseControllerServices.NewHash(SecurityModels.AppHashAlgorithm.SHA512)
                    .Update(data.Name ?? string.Empty, BinaryManagerModels.BinaryView.Base64),
                EnterpriseId = data.EnterpriseId ?? default
            });

            // achou usuário ?
            if (user is null)
                throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_USER_DONT_FOUND);

            // senha confere ?
            if (!this.baseControllerServices.NewPbkdf2().Verify(user.Password, data.Password ?? string.Empty, SecurityModels.Pbkdf2HashDerivation.HMACSHA512))
                throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_PASSWORD_INCORRECT);

            var rule = new AuthenticateRules.CodeRule { AuthId = user.UserId, CodeType = AccountDtos.CodeTypeEnum.AUTHENTICATION.intValue() };
            var code = this.service.Find(rule);

            if (code is null)
            {
                this.SendCodeEmail(this.service.Create(rule), user);
                return Ok(output.addResult(new AccountModels.SingInOutput { CodeSended = true }));
            }

            if (string.IsNullOrEmpty(data.Code))
                return Ok(output.addResult(new AccountModels.SingInOutput { CodeSended = true }));

            if (data.Code != code.Code || DateTime.UtcNow > code.ExpireIn)
                return BadRequest(output.addError(this.baseControllerServices.getMessage(null), "Code")); // [TODO]: adicionar mensagem

            this.service.Delete(rule);
            this.SendAuthenticatedEmail(code, user);
            this.baseControllerServices.jwtService.Write(new JwtModels.ClaimIdentifier { UserId = user.UserId.ToString(), EnterpriseId = user.EnterpriseId.ToString() }, out TokenCreated generated);

            output.addResult(new AccountModels.SingInOutput { Expire = generated.Expire, Token = generated.Token });
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

        return output.Failed ? BadRequest(output): Ok(output);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ResendCode([FromBody] AccountModels.ResendCodeInput data)
    {
        var output = new ControllerBaseModels.RequestResult<AccountModels.SendedOutput>();

        try
        {
            if (!this.baseControllerServices.validator.validate(data, output))
                return BadRequest(output);

            var code = this.service.Find(new AuthenticateRules.ResendCodeRule
            {
                Login = this.baseControllerServices.NewHash(SecurityModels.AppHashAlgorithm.SHA512)
                    .Update(data.Name ?? string.Empty, BinaryManagerModels.BinaryView.Base64),
                EnterpriseId = data.EnterpriseId ?? default,
                CodeType = AccountDtos.CodeTypeEnum.AUTHENTICATION.intValue()
            });

            if (code is not null)
            {
                this.SendResendCodeEmail(code);
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
        { }

        catch (AppDbException ex) 
        { }

        catch (Exception ex) 
        { }

        return output.Failed ? BadRequest(output) : Ok(output);
    }
}
