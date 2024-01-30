using Application.Validations;

namespace Personal.Service.Controllers.Models;

public static class PersonModels
{
    public class PersonPhysicalInput
    {
        public int Id { get; set; }

        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;

        [StringValidation(Required = true, MinLength = 5, MaxLength = 500, ErrorMessage = "")]
        public string CallName { get; set; } = string.Empty;

        [DateTimeValidation(Required = true, MinRange = -120, MaxRange = 0, ErrorMessage = "")]
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
        public int Version { get; set; }
    }

    public class RemovePersonPhysicalInput
    {
        [IntegerValidation(Required = true, Min = 1, ErrorMessage = "")]
        public int Id { get; set; }

        [IntegerValidation(Required = true, ErrorMessage = "")]
        public int Version { get; set; }
    }

    public class PersonJuridicalInput
    {
        public int Id { get; set; }
        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;

        [StringValidation(Required = true, MinLength = 3, MaxLength = 500, ErrorMessage = "")]
        public string CallName { get; set; } = string.Empty;

        [DateTimeValidation(Required = true, MinRange = -220, MaxRange = 0, ErrorMessage = "")]
        public DateTime Fundation { get; set; }
        public int Version { get; set; }
    }

    public class RemovePersonJuridicalInput
    {
        [IntegerValidation(Required = true, Min = 1, ErrorMessage = "")]
        public int Id { get; set; }

        [IntegerValidation(Required = true, ErrorMessage = "")]
        public int Version { get; set; }
    }
}
