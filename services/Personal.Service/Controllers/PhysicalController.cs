using Application.Base.Models;
using Application.Dtos;
using Application.Extensions;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal.Service.Controllers.Models;

namespace Personal.Service.Controllers;

[Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.AccessPersonPhysical))]
public class PhysicalController : ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;

    public PhysicalController(IBaseControllerServices baseControllerServices)
    {
        this.baseControllerServices = baseControllerServices;
    }

    [HttpGet]
    public IActionResult Index()
    {
        //var queryString = this.baseControllerServices.decodeQueryString<Teste>(HttpContext.Request.QueryString.ToString() ?? string.Empty);


        return Ok();
    }

    [HttpPost]
    public IActionResult Create([FromBody] PersonModels.PersonPhysicalInput input)
    {
        var output = new ControllerBaseModels.RequestResult<PersonDtos.PersonPhysical>();

        try
        {
            if (!this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            return Ok(output);
        }

        catch { }

        return Ok(output);
    }
}
