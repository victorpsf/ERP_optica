using Application.Base.Models;
using Application.Interfaces.Services;
using Authentication.Service.Controller.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

[AllowAnonymous]
public class AuthController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;

    public AuthController(IBaseControllerServices baseControllerServices)
    {
        this.baseControllerServices = baseControllerServices;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SingIn([FromBody] AuthModels.SingInInput data)
    {
        if (this.baseControllerServices.validator.validate(data, out List<ControllerBaseModels.ValidationError> errors))
            return BadRequest(new ControllerBaseModels.RequestResult<object> {  Errors = errors });

        return Ok(data);
    }
}
