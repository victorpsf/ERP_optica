using Application.Dtos;
using Personal.Service.Controllers.Models;

namespace Personal.Service.Controllers;

public partial class PhysicalController
{
    private PersonDtos.PersonPhysical Convert(PersonModels.PersonPhysicalInput input)
        => new PersonDtos.PersonPhysical
        {
            Name = input.Name,
            CallName = input.CallName,
            PersonType = PersonDtos.PersonType.Physical,
            BirthDate = input.BirthDate,
            Version = 0,
            EnterpriseId = this.baseControllerServices.loggedUser.Identifier.EnterpriseId
        };
}
