using Application.Dtos;
using Personal.Service.Controllers.Models;

namespace Personal.Service.Repositories.Rules;

public static class PersonRules
{
    public class CreatePersonRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public PersonModels.PersonPhysicalInput Input { get; set; } = new PersonModels.PersonPhysicalInput();
    }

    public class PersistPersonPhysicalRule: PersonDtos.PersonPhysical
    {
        public static PersistPersonPhysicalRule ByCreatePersonRule(CreatePersonRule rule) => new PersistPersonPhysicalRule
        {
            Name = rule.Input.Name,
            CallName = rule.Input.CallName,
            PersonType = PersonDtos.PersonType.Physical,
            BirthDate = rule.Input.BirthDate,
            EnterpriseId = rule.EnterpriseId,
            Version = 0
        };

        public PersonDtos.PersonPhysical toDto()
        { return (PersonDtos.PersonPhysical)this; }
    }
}
