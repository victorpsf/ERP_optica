using static Application.Library.ControllerModels;
using static Web.ApiM2.Controller.Models.PersonDocumentModels;
using static Application.Library.DocumentModels;
using Application.Messages;

namespace Web.ApiM2.Controller;

public partial class DocumentController
{
    public void Validate(DocumentType type, string Value, List<Failure> Errors)
    {
        switch (type)
        {
            case DocumentType.Cpf:
                break;
            default:
                Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.ERRO_DOCUMENT_TYPE_INVALID), Field = "Type" });
                break;
        }
    }

    public void Validate(CreatePersonDocumentInput input, List<Failure> Errors)
    {
        if (input is null) throw new Exception("ERRO_INVALID_INPUT_VALIDATION_FAILURE");

        if (input.Type == Application.Library.DocumentModels.DocumentType.Undefined)
            Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.ERRO_DOCUMENT_TYPE_NOT_INFORMED), Field = "Type" });
        else
            Validate(input.Type, input.Value, Errors);

        if (string.IsNullOrEmpty(input.Value))
            Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.ERRO_DOCUMENT_VALUE_NOT_INFORMED), Field = "Value" });

        if (!(input.PersonId > 0))
            Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.ERRO_DOCUMENT_PERSON_NOT_INFORMED), Field = "PersonId" });

        if (Errors.Any()) throw new Exception("VALIDATION_FAILURE");
    }
}
