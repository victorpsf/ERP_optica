using Authentication.Controller.Models;
using static Application.Library.AuthenticationModels;

namespace Authentication.Repositories.Rules
{
    public class AuthenticationRules
    {
        public enum FindType
        {
            Name = 1,
            Email = 2
        }

        public class FindUserRule
        { public AccountModels.SingInInput Input { get; set; } = new AccountModels.SingInInput(); }

        public class FindUserCodeRule 
        { public UserDto Input { get; set; } = new UserDto(); }

        public class CreateUserCode
        { 
            public UserDto Input { get; set; } = new UserDto();
            public CodeType Type { get; set;}
        }

        public class ChangeUserCodeRule
        {
            public UserCodeDto Input { get; set; } = new UserCodeDto();
        }

        public class RemoveUserCodeRule {
            public UserDto User { get; set; } = new UserDto();
            public UserCodeDto Code { get; set; } = new UserCodeDto();
        }
    }
}
