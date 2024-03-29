﻿namespace Application.Dtos;

public static class AccountDtos
{
    public class UserDto {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int EnterpriseId { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    public enum CodeTypeEnum
    {
        AUTHENTICATION = 1,
        FORGOTTEN = 2
    }

    public class CodeDto
    {
        public int AuthId { get; set; }
        public string Code { get; set; } = string.Empty;
        public CodeTypeEnum CodeType { get; set; }
        public DateTime ExpireIn { get; set; }
    }

    public class ResendDto
    {
        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class EnterpriseDto
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class EnterpriseOptionDto
    {
        public int Value { get; set; }
        public string Label { get; set; } = string.Empty;
    }

    public class ForgottenDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
