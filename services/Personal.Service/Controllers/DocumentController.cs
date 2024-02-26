using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Personal.Service.Repositories.Services;
using static Application.Base.Models.ControllerBaseModels;
using static Personal.Service.Controllers.Models.DocumentModels;

namespace Personal.Service.Controllers;

public class DocumentController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;
    private readonly DocumentRepoService documentRepoService;

    public DocumentController(IBaseControllerServices baseControllerServices, DocumentRepoService documentRepoService)
    {
        this.baseControllerServices = baseControllerServices;
        this.documentRepoService = documentRepoService;
    }

    [HttpPost]
    public IActionResult Index([FromBody] PaginationInput<DocumentInput> input)
    {
        var output = new RequestResult<PaginationOutput<DocumentInput, DocumentDtos.Document>>();

        try
        {
            output.Result = this.documentRepoService.Get(new Repositories.Rules.DocumentRules.FindDocumentWithPaginationRule
            {
                UserId = this.baseControllerServices.loggedUser.Identifier.UserId,
                EnterpriseId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
                Input = input,
                IntelligentSearch = input.IntelligentSearch
            });
        }

        catch (ControllerEmptyException) { }
        catch (BusinessException ex) 
        { output.addError(this.baseControllerServices.getMessage(ex.Stack), null); }
        catch (Exception ex) {
            this.baseControllerServices.logger.PrintsTackTrace(ex);
            output.addError(this.baseControllerServices.loggedUser.message.GetMessage(""), null);
        }

        return output.Failed ? BadRequest(output) : Ok(output);
    }

    [HttpPost]
    public IActionResult Save([FromBody] List<DocumentInput> input)
    {
        var output = new RequestResult<List<DocumentDtos.Document>>();


        try
        {
             if (this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            //var person = this.documentRepoService.Save(new Repositories.Rules.PersonRules.PersistPersonJuridicalRule
            //{
            //    EnterpriseId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
            //    UserId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId,
            //    Input = input
            //});

            //if (person is null)
            //    throw new ControllerEmptyException();

            //output.addResult(person);
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
    public IActionResult Delete()
    {
        return Ok();
    }
}
