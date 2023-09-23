using Application.Dtos;
using Authentication.Service.Repositories.Rules;

namespace Authentication.Service.Controllers.Models;

public static class AccountResultModels
{
    public class ValidateInputResult
    {
        public AccountDtos.UserDto User { get; set; } = new AccountDtos.UserDto();
        public AccountDtos.CodeDto? Code { get; set; }
        public AuthenticateRules.CodeRule Rule { get; set; } = new AuthenticateRules.CodeRule();
    }
}
