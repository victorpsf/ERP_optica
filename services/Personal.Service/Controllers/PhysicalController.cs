using Application.Base.Models;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Personal.Service.Controllers;

public class Teste
{

}

[Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.AccessPersonPhysical))]
public class PhysicalController : ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;

    public PhysicalController(IBaseControllerServices baseControllerServices)
    {
        this.baseControllerServices = baseControllerServices;
    }

    [HttpGet]
    public IActionResult Index([FromQuery] object body)
    {
        var queryString = this.baseControllerServices.decodeQueryString<Teste>(HttpContext.Request.QueryString.ToString() ?? string.Empty);
        return Ok(new { queryString });
    }
}
