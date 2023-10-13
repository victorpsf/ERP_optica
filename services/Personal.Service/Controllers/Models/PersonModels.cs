using Application.Validations;
using static Personal.Service.Controllers.Models.DocumentModels;

namespace Personal.Service.Controllers.Models;

public static class PersonModels
{
    public class PersonPhysicalInput
    {
        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;

        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string CallName { get; set; } = string.Empty;

        [DateTimeValidation(Required = true, MinRange = -120, MaxRange = 0, ErrorMessage = "")]
        public DateTime BirthDate { get; set; }

        [ListValidation<DocumentInput>(Required = true, Min = 1)]
        public List<DocumentInput> Documents { get; set; } = new List<DocumentInput>();
    }

    public class PersonJuridicalInput
    {
        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;

        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string CallName { get; set; } = string.Empty;

        [DateTimeValidation(Required = true, MinRange = -220, MaxRange = 0, ErrorMessage = "")]
        public DateTime Fundation { get; set; }
    }
}
