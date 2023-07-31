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

namespace Authentication.Service.Controllers;

[AllowAnonymous]
public class AccountController: ControllerBase
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

            var rule = new AuthenticateRules.SingInRule
            {
                Login = this.baseControllerServices.NewHash(SecurityModels.AppHashAlgorithm.SHA512)
                    .Update(data.Name ?? string.Empty, BinaryManagerModels.BinaryView.Base64),
                EnterpriseId = data.EnterpriseId ?? default
            };

            var user = this.service.Find(rule);

            if (user is null)
                throw new BusinessException("");

            if (!this.baseControllerServices.NewPbkdf2().Verify(user.Password, data.Password ?? string.Empty, SecurityModels.Pbkdf2HashDerivation.HMACSHA512))
                throw new BusinessException("");

            this.baseControllerServices.jwtService.Write(new JwtModels.ClaimIdentifier
            {
                UserId = user.UserId.ToString(),
                EnterpriseId = user.EnterpriseId.ToString()
            }, out TokenCreated generated);

            output.Result = new AccountModels.SingInOutput
            {
                Expire = generated.Expire,
                Token = generated.Token,
            };
        }

        catch (AppDbException ex) 
        { }

        catch (BusinessException ex)
        { }

        catch (Exception ex)
        { }

        return Ok(output);
    }
}
