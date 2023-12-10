using Application.Dtos;
using Personal.Service.Controllers.Models;

namespace Personal.Service.Repositories.Rules;

public static class PersonRules
{
    public class FindPersonPhysicalRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public PersonModels.FindPersonPhysicalInput Input { get; set; } = new PersonModels.FindPersonPhysicalInput();
    }

    public class PersistPersonPhysicalRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public PersonModels.PersonPhysicalInput Input { get; set; } = new PersonModels.PersonPhysicalInput();

        public FindPersonPhysicalRule FindById() => new FindPersonPhysicalRule
        {
            UserId = this.UserId,
            EnterpriseId = this.EnterpriseId,
            Input = new PersonModels.FindPersonPhysicalInput
            { Id = this.Input.Id  }
        };

        public FindPersonPhysicalRule ToFind() => new FindPersonPhysicalRule
        {
            UserId = this.UserId,
            EnterpriseId = this.EnterpriseId,
            Input = new PersonModels.FindPersonPhysicalInput
            {
                Id = this.Input.Id,
                Name = this.Input.Name,
                CallName = this.Input.CallName,
                BirthDate = this.Input.BirthDate,
                Version = this.Input.Version
            }
        };
    }
}
