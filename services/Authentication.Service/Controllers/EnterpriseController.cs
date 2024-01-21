
using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces.Services;
using Authentication.Service.Repositories.Rules;
using Authentication.Service.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

public class EnterpriseController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;
    private readonly AuthenticateRepoService service;

    public EnterpriseController(IBaseControllerServices baseControllerServices, AuthenticateRepoService service)
    {
        this.baseControllerServices = baseControllerServices;
        this.service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        var output = new ControllerBaseModels.RequestResult<List<AccountDtos.EnterpriseOptionDto>>();

        try
        {
            output.addResult(this.service.getEnterprises(new AuthenticateRules.EnterpriseRule { }).Select(a => new AccountDtos.EnterpriseOptionDto { Value = a.EnterpriseId, Label = a.Name }).ToList());
        }

        catch (BusinessException ex)
        { this.baseControllerServices.logger.PrintsTackTrace(ex); }

        catch (AppDbException ex)
        { this.baseControllerServices.logger.PrintsTackTrace(ex); }

        catch (Exception ex)
        { this.baseControllerServices.logger.PrintsTackTrace(ex); }

        return Ok(output);
    }
}
