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

        //public class FindUserCodeRule
        //{
        //    public int UserId { get; set; }
        //}

        //public class FindUserEnterpriseRule : FindUserRule { }

        //public class FindUserForgottenRule
        //{
        //    public string Name { get; set; } = string.Empty;
        //    public string Email { get; set; } = string.Empty;
        //    public FindType FindBy { get; set; }
        //}

        //public class FindCodeForgottenRule
        //{
        //    public int UserId { get; set; }
        //}

        //public class CodeForgottenBaseRule
        //{
        //    public int UserId { get; set; }
        //    public int Code { get; set; }
        //    public DateTime? SendedAt { get; set; }
        //    public DateTime? UsagedAt { get; set; }
        //}

        //public class CreateCodeToForgottenRule : CodeForgottenBaseRule { }
        //public class ChangeCodeToForgottenRule : CodeForgottenBaseRule { }

        //public class CreateUserCodeRule
        //{
        //    public int UserId { get; set; }
        //    public int Code { get; set; }
        //    public DateTime? UsagedAt { get; set; }
        //}

        //public class ChangeUserCodeRule
        //{
        //    public int UserId { get; set; }
        //    public int Code { get; set; }
        //    public DateTime? SendedAt { get; set; }
        //    public DateTime? UsagedAt { get; set; }
        //    public DateTime CreatedAt { get; set; }
        //}

        //public class ChangeUserPassword
        //{
        //    public int UserId { get; set; }
        //    public string Key { get; set; } = string.Empty;
        //}
    }
}
