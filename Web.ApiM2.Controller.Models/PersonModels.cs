using static Application.Library.PersonModels;

namespace Web.ApiM2.Controller.Models;

public static class PersonModels
{
    public class PersonInput
    {
        public string Name { get; set; } = string.Empty;
        public string? CallName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public PersonType PersonType { get; set; }
    }

    public class FindPerson: PersonInput {
        public int Id { get; set; }
    }
    public class CreatePerson: PersonInput { }
    public class UpdatePerson : FindPerson { }
    public class DeletePerson : FindPerson { }
}
