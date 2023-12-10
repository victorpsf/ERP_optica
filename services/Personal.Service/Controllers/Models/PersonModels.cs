using Application.Validations;

namespace Personal.Service.Controllers.Models;

public static class PersonModels
{
    public class FindPersonPhysicalInput
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CallName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Version { get; set; }
    }

    public class PersonPhysicalInput
    {
        public int Id { get; set; }

        [StringValidation(Required = true, MinLength = 10, MaxLength = 500, ErrorMessage = "")]
        public string Name { get; set; } = string.Empty;

        [StringValidation(Required = true, MinLength = 5, MaxLength = 500, ErrorMessage = "")]
        public string CallName { get; set; } = string.Empty;

        [DateTimeValidation(Required = true, MinRange = -120, MaxRange = 0, ErrorMessage = "")]
        public DateTime BirthDate { get; set; }
        public int Version { get; set; }

        //[ListValidation<DocumentInput>(Required = true, Min = 1)]
        //public List<DocumentInput> Documents { get; set; } = new List<DocumentInput>();

        //[ListValidation<ContactInput>(Required = true, Min = 1)]
        //public List<ContactInput> Contact { get; set; } = new List<ContactInput>();
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
