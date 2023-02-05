using Application.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ApiM2.Repositories;
using System.Net;
using static Application.Library.ControllerModels;
using Serilog;
using Shared.Services;
using Application.Messages;

namespace Web.ApiM2.Controller;

[Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.AccessPersonPhysical))]
public partial class PersonController: ControllerBase
{
    private readonly PersonPhysicalRepository Repository;
    private readonly LoggedUser LoggedUser;
    private readonly Messages messages;

    public PersonController(PersonPhysicalRepository repository, LoggedUser loggedUser, UserLanguage language)
    {
        this.Repository = repository;
        this.LoggedUser = loggedUser;
        this.messages = Messages.Create(language.language);
    }

    [Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.CreatePersonPhysical))]
    [HttpPost]
    public RequestResult<int, List<Failure>> Create([FromBody] Models.PersonModels.CreatePerson input)
    {
        var output = new RequestResult<int, List<Failure>> { Errors = new List<Failure>() };

        try
        {
            this.Validate(input, output.Errors);
            output.Result = this.Repository.Save(new Repositories.Rules.PersonRules.CreatePersonRule
            {
                Input = input,
                UserId = this.LoggedUser.Identifier.UserId,
                EnterpriseId = this.LoggedUser.Identifier.EnterpriseId
            });
        }

        catch (Exception error)
        {
            switch (error.Message)
            {
                case "ERRO_INVALID_INPUT_VALIDATION_FAILURE":
                    output.Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.INVALID_INPUT) });
                    break;
                case "PERSONID_IS_NULLABLE_OR_ZERO":
                    output.Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.PERSONID_IS_NULLABLE_OR_ZERO) });
                    break;
                default:
                    Log.Error(string.Format("PersonPhysicalController.Create :: {0}", error.Message));
                    break;
            }
        }

        return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest) : output;
    }

    [Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.AccessPersonPhysical))]
    [HttpPost]
    public RequestResult<List<PersonModels.PersonDto>, List<Failure>> List([FromBody] Models.PersonModels.FindPerson input)
    {
        var output = new RequestResult<List<PersonModels.PersonDto>, List<Failure>> { Errors = new List<Failure>() };

        try
        {
            output.Result = this.Repository.Find(new Repositories.Rules.PersonRules.FindPersonRule
            {
                Input = input,
                UserId = this.LoggedUser.Identifier.UserId,
                EnterpriseId = this.LoggedUser.Identifier.EnterpriseId
            });
        }

        catch (Exception error)
        {
            switch (error.Message)
            {
                default:
                    Log.Error(string.Format("PersonPhysicalController.Create :: {0}", error.Message));
                    break;
            }
        }

        return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest) : output;
    }
}
