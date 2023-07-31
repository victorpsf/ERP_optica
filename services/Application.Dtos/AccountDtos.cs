namespace Application.Dtos;

public static class AccountDtos
{
    public class UserDto {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int EnterpriseId { get; set; }
    }
}
