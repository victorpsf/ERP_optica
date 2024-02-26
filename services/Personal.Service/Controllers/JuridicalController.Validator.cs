using Application.Base.Models;
using Application.Dtos;
using Application.Extensions;
using Application.Utils;
using static Personal.Service.Controllers.Models.PersonModels;

namespace Personal.Service.Controllers;

public partial class JuridicalController
{
    private void validate (PersonJuridicalInput input, ControllerBaseModels.RequestResult<PersonDtos.PersonJuridical> output)
    {
        if (this.baseControllerServices.validator.validate(input, output))
            throw new ControllerEmptyException();

        if (this.baseControllerServices.validator.validate(input.Documents, output))
            throw new ControllerEmptyException();

        if (!input.Documents.Where(a => a.DocumentType == DocumentDtos.DocumentType.CNPJ).Where(a => DocumentValidation.validateCNPJ(a.Value)).Any())
        {
            output.addError("", "Documents");
            throw new ControllerEmptyException();
        }
    }
}
