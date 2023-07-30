namespace Application.Base.Models;

public static class PermissionModels
{
    public static List<string> AllPermissions()
    {
        List<string> permissionNames = new List<string>();
        foreach (Type enumType in typeof(PermissionModels).GetNestedTypes())
            if (enumType.IsEnum) permissionNames.AddRange(Enum.GetNames(enumType));
        return permissionNames;
    }

    public enum AccountPermission
    {
        AccessAccountPermission = 1,
        CreateAccountPermission = 2,
        UpdateAccountPermission = 3,
        RemoveAccountPermission = 4
    }

    public enum PersonJuridicalPermission
    {
        AccessPersonJuridical = 1,
        CreatePersonJuridical = 2,
        UpdatePersonJuridical = 3,
        RemovePersonJuridical = 4
    }

    public enum PersonPhysicalPermission
    {
        AccessPersonPhysical = 1,
        CreatePersonPhysical = 2,
        UpdatePersonPhysical = 3,
        RemovePersonPhysical = 4
    }

    public enum ClientPermission
    {
        AccessClient = 1,
        CreateClient = 2,
        UpdateClient = 3,
        RemoveClient = 4
    }

    public enum EmployeePermission
    {
        AccessEmployee = 1,
        CreateEmployee = 2,
        UpdateEmployee = 3,
        RemoveEmployee = 4
    }

    public enum EnterprisePermission
    {
        AccessEnterprise = 1,
        CreateEnterprise = 2,
        UpdateEnterprise = 3,
        RemoveEnterprise = 4
    }

    public enum DocumentPermission
    {
        AccessDocument = 1,
        CreateDocument = 2,
        UpdateDocument = 3,
        RemoveDocument = 4
    }

    public enum ContactPermission
    {
        AccessContact = 1,
        CreateContact = 2,
        UpdateContact = 3,
        RemoveContact = 4
    }

    public enum AddressPermission
    {
        AccessAddress = 1,
        CreateAddress = 2,
        UpdateAddress = 3,
        RemoveAddress = 4
    }

    public class PermissionDto
    {
        public int PermissionId { get; set; }
        public string Value { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }
    }

    public class UserPermissionDto
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
}
