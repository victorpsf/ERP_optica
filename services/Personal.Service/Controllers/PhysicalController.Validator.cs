using Application.Base.Models;
using Application.Dtos;
using Application.Extensions;
using Application.Utils;
using static Personal.Service.Controllers.Models.PersonModels;

namespace Personal.Service.Controllers
{
    public partial class PhysicalController
    {
        private void validate(PersonPhysicalInput input, ControllerBaseModels.RequestResult<PersonDtos.PersonPhysical> output)
        {
            if (this.baseControllerServices.validator.validate(input, output))
                throw new ControllerEmptyException();

            if (this.baseControllerServices.validator.validate(input.Documents, output))
                throw new ControllerEmptyException();

            if (!input.Documents.Where(a => a.DocumentType == DocumentDtos.DocumentType.CPF).Where(a => DocumentValidation.ValidateCpf(a.Value)).Any())
            {
                output.addError("", "Documents");
                throw new ControllerEmptyException();
            }
        }
    }
}
