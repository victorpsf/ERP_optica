using Application.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using Web.ApiM1.Repositories;
using static Application.Library.ControllerModels;
using static Web.ApiM1.Controller.Models.EnterpriseModels;

namespace Web.ApiM1.Controller
{
    public partial class EnterpriseController : ControllerBase
    {
        private readonly EnterpriseRepository Repository;

        public EnterpriseController(EnterpriseRepository repository) { this.Repository = repository; }

        [AllowAnonymous]
        [HttpPost]
        public RequestResult<List<EnterpriseModels.EnterpriseDto>, List<Failure>> List([FromBody] ListEnterprisesInput input)
        {
            var output = new RequestResult<List<EnterpriseModels.EnterpriseDto>, List<Failure>> { Errors = new List<Failure>() };

            try
            {
                output.Result = this.Repository.Get(new Repositories.Rules.EnterpriseRules.GetEnteprisesRule
                {
                    Input = input ?? new ListEnterprisesInput()
                });
            }

            catch (Exception error)
            {
                switch (error.Message)
                {
                    default:
                        Log.Error(string.Format("EnterpriseController.List :: {0}", error.Message));
                        break;
                }
            }

            return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest): output;
        }
    }
}