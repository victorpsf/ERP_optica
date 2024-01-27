using Application.Base.Models;
using Application.Dtos;
using Application.Extensions;
using Personal.Service.Controllers.Models;
using static Personal.Service.Controllers.Models.PersonModels;

namespace Personal.Service.Repositories.Rules;

public static class PersonRules
{
    public class FindPersonPhysicalWithPaginationRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public bool IntelligentSearch { get; set; }
        public PaginationInput<PersonPhysicalInput> Input { get; set; } = new PaginationInput<PersonPhysicalInput>(); 

        public PersonPhysicalInput Search { get => this.Input.Search; }
    }

    public class FindPersonPhysicalRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public FindPersonPhysicalInput Input { get; set; } = new FindPersonPhysicalInput();
    }

    public class PersistPersonPhysicalDtoRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public PersonDtos.PersonPhysical? Input { get; set; }
    }

    public class PersistPersonPhysicalRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public PersonPhysicalInput Input { get; set; } = new PersonPhysicalInput();

        public PersonDtos.PersonPhysical ToDto(PersonDtos.PersonPhysical? dto)
            => new PersonDtos.PersonPhysical
            {
                Id = (dto is not null) ? dto.Id: this.Input.Id,
                Name = (dto is not null && string.IsNullOrEmpty(this.Input.Name)) ? dto.Name: (this.Input.Name ?? string.Empty),
                PersonType = dto is not null ? dto.PersonType: PersonDtos.PersonType.Physical,
                CallName = (dto is not null && string.IsNullOrEmpty(this.Input.CallName)) ? dto.CallName: (this.Input.CallName ?? string.Empty),
                BirthDate = (dto is not null && !this.Input.BirthDate.InRange(-120, 0)) ? dto.BirthDate: this.Input.BirthDate,
                EnterpriseId = dto is not null ? dto.EnterpriseId: this.EnterpriseId,
                Version = dto is not null ? (dto.Version + 1) : this.Input.Version
            };

        public FindPersonPhysicalRule FindById() => new FindPersonPhysicalRule
        {
            UserId = this.UserId,
            EnterpriseId = this.EnterpriseId,
            Input = new FindPersonPhysicalInput
            { Id = this.Input.Id  }
        };

        public FindPersonPhysicalRule ToFind() => new FindPersonPhysicalRule
        {
            UserId = this.UserId,
            EnterpriseId = this.EnterpriseId,
            Input = new FindPersonPhysicalInput
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
