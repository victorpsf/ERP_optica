using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Controller.Models;
using Application.Exceptions;

namespace Service.Authentication.Controllers;

[AllowAnonymous]
public partial class AccountController: ControllerBase
{
    private IAppControllerServices services;

    public AccountController(IAppControllerServices services) {
        this.services = services;
    }

    [HttpPost]
    public IActionResult SingIn([FromBody] AccountModels.SingInInput input)
    {
        try
        {
            this.Validate(input);

            

            return Ok();
        }

        catch (AppValidationException error)
        {
            return BadRequest(
                AppRequestResult<object>.Failed(error.Erros)
            );
        }

        catch (Exception ex)
        {
            this.services.logger.Error("AccountController.SingIn", ex);
            return this.Problem();
        }
    }
}
