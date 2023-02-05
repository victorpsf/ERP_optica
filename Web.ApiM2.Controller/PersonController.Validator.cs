using Web.ApiM2.Controller.Models;
using static Application.Library.ControllerModels;
using Shared.Extensions;
using Application.Messages;

namespace Web.ApiM2.Controller;

public partial class PersonController
{
    private void ValidatePersonPhysical(PersonModels.PersonInput input, List<Failure> Errors)
    {
        if (string.IsNullOrEmpty(input.CallName) == false)
        { if (!(input.CallName.Length > 12 && input.CallName.Length < 500)) Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.CALLNAME_RULE_LENGTH), Field = "CallName" }); }

        if (input.CreatedAt.HasValue)
        {
            if (input.CreatedAt.IsLowerOrEqual(120) == false) Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.BIRTHDATE_RULE_LENGTH), Field = "CreatedAt" });
        }

        else Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.BIRTHDATE_RULE), Field = "CreatedAt" });
    }

    private void ValidatePersonJuridical(PersonModels.PersonInput input, List<Failure> Errors)
    {
        if (string.IsNullOrEmpty(input.CallName) == false)
        { if (!(input.CallName.Length > 12 && input.CallName.Length < 500)) Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.CALLNAME_RULE_LENGTH), Field = "CallName" }); }

        else Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.CALLNAME_RULE), Field = "CallName" });

        if (input.CreatedAt.HasValue)
        {
            if (input.CreatedAt.IsLowerOrEqual(120) == false) Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.CREATEDAT_RULE_LENGTH), Field = "CreatedAt" });
        }
    }

    public void Validate(PersonModels.CreatePerson input, List<Failure> Errors)
    {
        if (input is null) throw new Exception("ERRO_INVALID_INPUT_VALIDATION_FAILURE");


        if (string.IsNullOrEmpty(input.Name) == false)
        { if (!(input.Name.Length > 12 && input.Name.Length < 500)) Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.NAME_RULE_LENGTH), Field = "Name" }); }

        else Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.NAME_RULE), Field = "Name" });

        switch (input.PersonType)
        {
            case Application.Library.PersonModels.PersonType.Physical:
                this.ValidatePersonPhysical(input, Errors);
                break;
            case Application.Library.PersonModels.PersonType.Juridical:
                this.ValidatePersonJuridical(input, Errors);
                break;
            default:
                Errors.Add(new Failure { Message = this.messages.GetMessage(MessagesEnum.INVALID_PERSON_TYPE), Field = "PersonType" });
                break;
        }

        if (Errors.Any()) throw new Exception("VALIDATION_FAILURE");
    }
}
