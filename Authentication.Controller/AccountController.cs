using Authentication.Controller.Models;
using Authentication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using Shared.Services;
using System.Net;
using static Application.Library.ControllerModels;
using static Application.Library.Security.SecurityModels;
using static Authentication.Repositories.Rules.AuthenticationRules;

namespace Authentication.Controller
{
    public partial class AccountController : ControllerBase
    {
        private readonly AuthenticationRepository Repository;
        private readonly JwtService Jwt;
        private readonly IConfiguration Configuration;

        public AccountController(AuthenticationRepository repository, JwtService jwt, IConfiguration configuration)
        {
            this.Repository = repository;
            this.Jwt = jwt;
            this.Configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public RequestResult<AccountModels.SingInOutput, List<Failure>> SingIn([FromBody] AccountModels.SingInInput input)
        {
            var output = new RequestResult<AccountModels.SingInOutput, List<Failure>> { Errors = new List<Failure>() };

            try
            {
                Validate(input, output.Errors);

                var user = this.Repository.Find(
                    new FindUserRule
                    {
                        Input = new AccountModels.SingInInput
                        {
                            Name = Security.CreateHash(HashType.SHA512).Update(input.Name).Digest(Application.Library.Utilities.BinaryConverter.StringView.BASE64),
                            EnterpriseId = input.EnterpriseId,
                        }
                    }
                );

                var code = this.GetCodeToUser(user);
                if (!this.Validate(user.Key, input.Key)) 
                    throw new Exception("USER_AND_KEY_INVALID");

                if (code  is null) {
                    code = this.Repository.Save(new CreateUserCode {
                        Input = user,
                        Type = Application.Library.AuthenticationModels.CodeType.Authentication
                    });
                    this.SendCodeEmail(code, user);

                    return output.SetResult( new AccountModels.SingInOutput { Step = AccountModels.AuthenticationSteps.CODE_SENDED } )
                        .SetStatusCode(HttpStatusCode.Created);
                }

                if (
                        !this.Validate(input.Code) 
                    ||  !this.Validate(input.GetCode(), code.Code)
                ) throw new Exception("USER_AND_KEY_AND_CODE_INVALID");

                this.Jwt.Write(
                    new Application.Library.JwtModels.ClaimIdentifier { UserId = user.UserId.ToString(), EnterpriseId = user.EnterpriseId.ToString() }, 
                    out Application.Library.JwtModels.TokenCreated info
                );

                this.Repository.Remove(new RemoveUserCodeRule { User = user, Code = code });
                this.SendAuthenticatedEmail(code, user);
                output.SetResult(new AccountModels.SingInOutput { Step = AccountModels.AuthenticationSteps.AUTHENTICATED, Token = info.Token, ExpirationTime = info.Expire });
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "USER_AND_KEY_INVALID":
                    case "USER_AND_KEY_AND_CODE_INVALID":
                        output.Errors.Add(new Failure { Message = Messages.USER_AND_KEY_AND_CODE_INVALID });
                        break;
                    case "USER_DONT_FOUND":
                        output.Errors.Add(new Failure { Message = Messages.USER_DONT_FOUND });
                        break;
                    case "VALIDATION_FAILURE":
                        break;
                    case "NOT_POSSIBLE_CREATE_CODE":
                        output.Errors.Add(new Failure { Message = Messages.NOT_POSSIBLE_CREATE_CODE });
                        break;
                    default:
                        Log.Error(string.Format("AccountController.SingIn :: {0}", ex.Message));
                        break;
                }
            }

            return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest): output;
        }

        [AllowAnonymous]
        [HttpPost]
        public RequestResult<object, List<Failure>> ResendEmail([FromBody] AccountModels.ResendEmailInput input)
        {
            var output = new RequestResult<object, List<Failure>> { Errors = new List<Failure>() };

            try
            {
                Validate(input, output.Errors);

                var user = this.Repository.Find(
                    new FindUserRule
                    {
                        Input = new AccountModels.SingInInput
                        {
                            Name = Security.CreateHash(HashType.SHA512).Update(input.Name).Digest(Application.Library.Utilities.BinaryConverter.StringView.BASE64),
                            EnterpriseId = input.EnterpriseId,
                        }
                    }
                );

                var userCode = this.GetCodeToUser(user);
                if (userCode is null) return output;
                this.SendCodeEmail(userCode, user);
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "USER_AND_KEY_AND_CODE_INVALID":
                        output.Errors.Add(new Failure { Message = "Usuário, senha ou código inválido" });
                        break;
                    case "USER_AND_KEY_INVALID":
                    case "USER_DONT_FOUND":
                        output.Errors.Add(new Failure { Message = "Usuário ou senha inválido" });
                        break;
                    case "VALIDATION_FAILURE":
                        break;
                    default:
                        Log.Error(string.Format("AccountController.SingIn :: {0}", ex.Message));
                        break;
                }
            }

            return output.Errors.Any() ? output.SetStatusCode(HttpStatusCode.BadRequest): output;
        }
    }
}
