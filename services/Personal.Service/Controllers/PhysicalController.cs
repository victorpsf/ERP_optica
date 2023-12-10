using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal.Service.Controllers.Models;
using Personal.Service.Repositories.Services;

namespace Personal.Service.Controllers;

[Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.AccessPersonPhysical))]
public partial class PhysicalController : ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;
    private readonly PersonRepoService personRepoService;

    public PhysicalController(IBaseControllerServices baseControllerServices, PersonRepoService personRepoService)
    {
        this.baseControllerServices = baseControllerServices;
        this.personRepoService = personRepoService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] PersonModels.PersonPhysicalInput input)
    {
        var output = new ControllerBaseModels.RequestResult<PersonDtos.PersonPhysical>();

        try
        {
            if (this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            var person = this.personRepoService.Create(new Repositories.Rules.PersonRules.PersistPersonPhysicalRule
            {
                EnterpriseId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
                UserId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
                Input = input
            });

            if (person is null) 
                throw new ControllerEmptyException();

            return Ok(output.addResult(person));
        }

        catch { }

        return BadRequest(output);
    }
}
