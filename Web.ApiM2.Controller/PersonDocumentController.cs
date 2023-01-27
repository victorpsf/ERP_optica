using Application.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Services;
using System.Net;
using Web.ApiM2.Repositories;
using static Application.Library.ControllerModels;
using static Application.Library.DocumentModels;
using static Web.ApiM2.Controller.Models.PersonDocumentModels;

namespace Web.ApiM2.Controller
{
    [Authorize(Policy = nameof(PermissionModels.DocumentPermission.AccessDocument))]
    public partial class PersonDocumentController: ControllerBase
    {
        private readonly PersonDocumentRepository Repository;
        private readonly LoggedUser LoggedUser;

        public PersonDocumentController(PersonDocumentRepository repository, LoggedUser loggedUser) { 
            this.Repository = repository;
            this.LoggedUser = loggedUser;
        }

        [Authorize(Policy = nameof(PermissionModels.DocumentPermission.CreateDocument)), Authorize(Policy = nameof(PermissionModels.DocumentPermission.UpdateDocument))]
        [HttpPost]
        public RequestResult<DocumentDto, List<Failure>> CreateV1([FromBody] CreatePersonDocumentInput input)
        {
            var output = new RequestResult<DocumentDto, List<Failure>> { Errors = new List<Failure>() };

            try
            {
                this.Validate(input, output.Errors);
                int personDocumentId = this.Repository.Save(new Repositories.Rules.PersonDocumentRules.CreatePersonDocumentRule
                {
                    Input = input,
                    PersonType = PersonModels.PersonType.Physical,
                    UserId = this.LoggedUser.Identifier.UserId,
                    EnterpriseId = this.LoggedUser.Identifier.EnterpriseId
                });
            }

            catch(Exception ex)
            {
                switch(ex.Message)
                {
                    default:
                        Log.Error(string.Format("PersonDocumentController.CreateV1 :: {0}", ex.Message));
                        break;
                }
            }

            return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest): output;
        }

        [Authorize(Policy = nameof(PermissionModels.DocumentPermission.CreateDocument)), Authorize(Policy = nameof(PermissionModels.DocumentPermission.UpdateDocument))]
        [HttpPost]
        public RequestResult<DocumentDto, List<Failure>> CreateV2([FromBody] CreatePersonDocumentInput input)
        {
            var output = new RequestResult<DocumentDto, List<Failure>> { Errors = new List<Failure>() };

            try
            {
                this.Validate(input, output.Errors);
                int personDocumentId = this.Repository.Save(new Repositories.Rules.PersonDocumentRules.CreatePersonDocumentRule
                {
                    Input = input,
                    PersonType = PersonModels.PersonType.Juridical,
                    UserId = this.LoggedUser.Identifier.UserId,
                    EnterpriseId = this.LoggedUser.Identifier.EnterpriseId
                });
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    default:
                        Log.Error(string.Format("PersonDocumentController.CreateV1 :: {0}", ex.Message));
                        break;
                }
            }

            return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest): output;
        }
    }
}
