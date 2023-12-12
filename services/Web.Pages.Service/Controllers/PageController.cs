using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Web.Pages.Service.Controllers.Models.PageModels;

namespace Web.Pages.Service.Controllers;

[AllowAnonymous]
public class PageController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;

    public PageController(IBaseControllerServices baseControllerServices)
    {
        this.baseControllerServices = baseControllerServices;
    }

    [HttpPost]
    public IActionResult List([FromBody] FindPageInput input)
    {
        return Ok();
    }
}
