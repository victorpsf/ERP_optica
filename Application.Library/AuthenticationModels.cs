namespace Application.Library
{
    public static class AuthenticationModels
    {
        public enum CodeType {
            Authentication = 1,
            Forgotten = 2
        }

        public class LoggedUserDto
        {
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
            public string Token { get; set; } = string.Empty;
        }

        public class UserDto
        {
            public int UserId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Key { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public int EnterpriseId { get; set; }
        }

        public class UserCodeDto
        {
            public long CodeId { get; set; }
            public int UserId { get; set; }
            public int Code { get; set; }
            public CodeType Type { get; set; }
            public DateTime ExpireIn { get; set;}
        }

        public class ForgottenDto
        {
            public int UserId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        public class ForgottenCodeDto
        {
            public int UserId { get; set; }
            public int Code { get; set; }
            public DateTime? SendedAt { get; set; }
            public DateTime? UsagedAt { get; set; }
        }
    }
}
