using Application.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ApiM2.Repositories;
using Web.ApiM2.Controller.Models;
using System.Net;
using static Application.Library.ControllerModels;
using Serilog;
using Shared.Services;

namespace Web.ApiM2.Controller
{
    [Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.AccessPersonPhysical))]
    public partial class PersonPhysicalController: ControllerBase
    {
        private readonly PersonPhysicalRepository Repository;
        private readonly LoggedUser LoggedUser;

        public PersonPhysicalController(PersonPhysicalRepository repository, LoggedUser loggedUser)
        {
            this.Repository = repository;
            this.LoggedUser = loggedUser;
        }

        [Authorize(Policy = nameof(PermissionModels.PersonPhysicalPermission.CreatePersonPhysical))]
        [HttpPost]
        public RequestResult<PersonModels.PersonPhysicalDto?, List<Failure>> Create([FromBody] PersonPhysicalModels.CreatePersonPhysical input)
        {
            var output = new RequestResult<PersonModels.PersonPhysicalDto?, List<Failure>> { Errors = new List<Failure>() };

            try
            {
                this.Validate(input, output.Errors);
                int personId = this.Repository.Save(new Repositories.Rules.PersonPhysicalRules.CreatePersonPhysicalRule
                {
                    Input = input,
                    UserId = this.LoggedUser.Identifier.UserId,
                    EnterpriseId = this.LoggedUser.Identifier.EnterpriseId
                });
                output.Result = this.Repository.Find(new Repositories.Rules.PersonPhysicalRules.FindPersonPhysicalRule
                {
                    Input = personId,
                    UserId = this.LoggedUser.Identifier.UserId,
                    EnterpriseId = this.LoggedUser.Identifier.EnterpriseId
                });
            }

            catch (Exception error)
            {
                switch (error.Message)
                {
                    case "PERSONID_IS_NULLABLE_OR_ZERO":
                        break;
                    default:
                        Log.Error(string.Format("PersonPhysicalController.Create :: {0}", error.Message));
                        break;
                }
            }

            return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest): output;
        }
    }
}
