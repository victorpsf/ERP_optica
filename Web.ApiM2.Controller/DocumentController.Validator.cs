using static Application.Library.ControllerModels;
using static Web.ApiM2.Controller.Models.PersonDocumentModels;

namespace Web.ApiM2.Controller
{
    public partial class DocumentController
    {
        public void Validate(Application.Library.DocumentModels.DocumentType type, string Value, List<Failure> Errors)
        {

        }

        public void Validate(CreatePersonDocumentInput input, List<Failure> Errors)
        {
            if (input is null)
            {
                Errors.Add(new Failure { Message = "Dados de entrada inválido" });
                throw new Exception("VALIDATION_FAILURE");
            }

            if (input.Type == Application.Library.DocumentModels.DocumentType.Undefined)
                Errors.Add(new Failure { Message = "Tipo de documento não foi informado", Field = "Type" });
            else
                Validate(input.Type, input.Value, Errors);

            if (string.IsNullOrEmpty(input.Value))
                Errors.Add(new Failure { Message = "Tipo de documento não foi informado", Field = "Value" });

            if (!(input.PersonId > 0))
                Errors.Add(new Failure { Message = "Pessoa fisica deve ser informado", Field = "PersonId" });

            if (Errors.Any()) throw new Exception("VALIDATION_FAILURE");
        }
    }
}
