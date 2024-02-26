using static Application.Base.Models.BasePatternsModels;

namespace Application.Dtos;

public static class PersonDtos
{
    public enum PersonType {
        Physical = 1,
        Juridical = 2
    }


    public class Person: BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PersonType PersonType { get; set; }
        public string CallName { get; set; } = string.Empty;
        public int EnterpriseId { get; set; }
        public int Version { get; set; }

        public List<DocumentDtos.Document> Documents { get; set; } = new List<DocumentDtos.Document>();
    }

    public class PersonPhysical: Person
    { public DateTime BirthDate { get; set; } }

    public class PersonJuridical : Person
    { public DateTime Fundation { get; set; } }
}
