using Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Personal.Service.Controllers.Models;

public static class PersonModels
{
    public class PersonPhysicalInput
    {
        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;

        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string CallName { get; set; } = string.Empty;

        [IntegerValidation(Min = 1, Required = true, ErrorMessage = "")]
        public int EnterpriseId { get; set; }

        [DateTimeValidation(Required = true, MinRange = -120, MaxRange = 0, ErrorMessage = "")]
        public DateTime BirthDate { get; set; }
    }

    public class PersonJuridicalInput
    {
        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;

        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string CallName { get; set; } = string.Empty;

        [IntegerValidation(Min = 1, Required = true, ErrorMessage = "")]
        public int EnterpriseId { get; set; }

        [DateTimeValidation(Required = true, MinRange = -220, MaxRange = 0, ErrorMessage = "")]
        public DateTime Fundation { get; set; }
    }
}
