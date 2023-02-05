using static Application.Library.DocumentModels;
using static Application.Library.PersonModels;

namespace Web.ApiM2.Controller.Models;

public static class PersonDocumentModels
{
    public class BasePersonDocumentInput
    {
        public PersonType PersonType { get; set; }
        public DocumentType Type { get; set; }
        public int PersonId { get; set; }
        public string Value { get; set; } = string.Empty;
    }


    public class CreatePersonDocumentInput: BasePersonDocumentInput { }

    public class ChangePersonDocumentInput: CreatePersonDocumentInput
    {
        public int Id { get; set; }
    }

    public class ListPersonDocumentInput: ChangePersonDocumentInput { }
}
