﻿using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Personal.Service.Repositories.Services;
using static Personal.Service.Controllers.Models.PersonModels;

namespace Personal.Service.Controllers;

public partial class JuridicalController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;
    private readonly PersonRepoService personRepoService;

    public JuridicalController(
        IBaseControllerServices baseControllerServices, 
        PersonRepoService personRepoService
    ) {
        this.baseControllerServices = baseControllerServices;
        this.personRepoService = personRepoService;
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromBody] PaginationInput<PersonJuridicalInput> input)
    {
        var output = new ControllerBaseModels.RequestResult<PaginationOutput<PersonJuridicalInput, PersonDtos.PersonJuridical>>();

        try
        {
            output.Result = await this.personRepoService.Get(
                new Repositories.Rules.PersonRules.FindPersonJuridicalWithPaginationRule
                {
                    IntelligentSearch = input.IntelligentSearch,
                    EnterpriseId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
                    UserId = this.baseControllerServices.loggedUser.Identifier.UserId,
                    Input = input
                }
            ); ;
        }

        catch (ControllerEmptyException) { }
        catch (BusinessException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (AppDbException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (Exception ex)
        {
            this.baseControllerServices.logger.PrintsTackTrace(ex);
            output.addError(this.baseControllerServices.loggedUser.message.GetMessage(""), null);
        }

        return output.Failed ? BadRequest(output) : Ok(output);
    }

    [HttpPost]
    public IActionResult Save([FromBody] PersonJuridicalInput input)
    {
        var output = new ControllerBaseModels.RequestResult<PersonDtos.PersonJuridical>();

        try
        {
            this.validate(input, output);

            var person = this.personRepoService.Save(new Repositories.Rules.PersonRules.PersistPersonJuridicalRule
            {
                EnterpriseId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
                UserId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
                Input = input
            });

            if (person is null)
                throw new ControllerEmptyException();

            output.addResult(person);
        }

        catch (ControllerEmptyException) { }
        catch (BusinessException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (AppDbException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (Exception ex)
        {
            this.baseControllerServices.logger.PrintsTackTrace(ex);
            output.addError(this.baseControllerServices.loggedUser.message.GetMessage(""), null);
        }

        return output.Failed ? BadRequest(output) : Ok(output);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] RemovePersonJuridicalInput input)
    {
        var output = new ControllerBaseModels.RequestResult<PersonDtos.PersonJuridical>();

        try
        {
            if (this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            output.Result = this.personRepoService.Remove(new Repositories.Rules.PersonRules.RemovePersonJuridicalRule
            {
                UserId = this.baseControllerServices.loggedUser.Identifier.UserId,
                EnterpriseId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
                Input = input
            });
        }

        catch (ControllerEmptyException) { }
        catch (BusinessException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (AppDbException ex)
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (Exception ex)
        {
            this.baseControllerServices.logger.PrintsTackTrace(ex);
            output.addError(this.baseControllerServices.loggedUser.message.GetMessage(""), null);
        }

        return output.Failed ? BadRequest(output) : Ok(output);
    }
}
