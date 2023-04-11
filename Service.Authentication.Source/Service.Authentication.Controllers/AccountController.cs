using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Service.Authentication.Controllers;

[AllowAnonymous]
public partial class AccountController: ControllerBase
{
    private IAppControllerServices services;

    public AccountController(IAppControllerServices services) {
        this.services = services;
    }

    [HttpPost]
    public IActionResult SingIn([FromBody] object value)
    {
        try
        {

        }

        catch (Exception ex)
        {

        }

        return Ok(value);
    }
}
