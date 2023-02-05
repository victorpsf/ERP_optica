using Application.Library;
using Application.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Services;
using System.Net;
using Web.ApiM2.Repositories;
using static Application.Library.ControllerModels;
using static Application.Library.DocumentModels;
using static Web.ApiM2.Controller.Models.PersonDocumentModels;

namespace Web.ApiM2.Controller;

[Authorize(Policy = nameof(PermissionModels.DocumentPermission.AccessDocument))]
public partial class DocumentController: ControllerBase
{
    private readonly PersonDocumentRepository Repository;
    private readonly LoggedUser LoggedUser;
    private readonly Messages messages;

    public DocumentController(PersonDocumentRepository repository, LoggedUser loggedUser, UserLanguage language) { 
        this.Repository = repository;
        this.LoggedUser = loggedUser;
        this.messages = Messages.Create(language.language);
    }

    [Authorize(Policy = nameof(PermissionModels.DocumentPermission.CreateDocument)), Authorize(Policy = nameof(PermissionModels.DocumentPermission.UpdateDocument))]
    [HttpPost]
    public RequestResult<DocumentDto, List<Failure>> Create([FromBody] CreatePersonDocumentInput input)
    {
        var output = new RequestResult<DocumentDto, List<Failure>> { Errors = new List<Failure>() };

        try
        {
            this.Validate(input, output.Errors);
            int personDocumentId = this.Repository.Save(
                new Repositories.Rules.PersonDocumentRules.CreatePersonDocumentRule
                {
                    Input = input,
                    PersonType = input.PersonType,
                    UserId = this.LoggedUser.Identifier.UserId,
                    EnterpriseId = this.LoggedUser.Identifier.EnterpriseId
                }
            );
        }

        catch(Exception ex)
        {
            switch(ex.Message)
            {
                case "ERRO_INVALID_INPUT_VALIDATION_FAILURE":
                    output.Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.ERRO_INVALID_INPUT) });
                    break;
                case "ERRO_DOCUMENTTYPE_EXISTS":
                    output.Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.ERRO_DOCUMENTTYPE_EXISTS) });
                    break;
                case "ERRO_DOCUMENT_NOT_EXISTS":
                    output.Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.ERRO_DOCUMENT_NOT_EXISTS) });
                    break;
                default:
                    Log.Error(string.Format("PersonDocumentController.CreateV1 :: {0}", ex.Message));
                    break;
            }
        }

        return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest): output;
    }
}
