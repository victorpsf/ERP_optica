using Web.ApiM2.Controller.Models;
using static Application.Library.ControllerModels;
using Shared.Extensions;

namespace Web.ApiM2.Controller
{
    public partial class PersonPhysicalController
    {
        public void Validate(PersonPhysicalModels.CreatePersonPhysical input, List<Failure> Errors)
        {
            if (input is null)
            {
                Errors.Add(new Failure { Message = "Dado de entrada inválido" });
                throw new Exception("VALIDATION_FAILURE");
            }


            if (string.IsNullOrEmpty(input.Name))
                Errors.Add(new Failure { Message = "Nome é um campo obrigatorio", Field = "Name" });

            else
            {
                if (!(input.Name.Length > 12 && input.Name.Length < 500))
                    Errors.Add(new Failure { Message = "Nome deve ter mais de 12 caracteres e menos de 500 caracteres", Field = "Name" });
            }

            if (input.BirthDate is null)
                Errors.Add(new Failure { Message = "Data de Nascimento é obrigatorio", Field = "BirthDate" });
            else
            {
                if (!input.BirthDate.InRangeYears(120))
                    Errors.Add(new Failure { Message = "Data de Nascimento não pode ser 120 anos maior ou menor que a data atual", Field = "BirthDate" });
            }

            if (Errors.Any()) throw new Exception("VALIDATION_FAILURE");
        }
    }
}
