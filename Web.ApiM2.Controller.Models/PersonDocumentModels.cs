using static Application.Library.DocumentModels;
using static Application.Library.PersonModels;

namespace Web.ApiM2.Controller.Models
{
    public static class PersonDocumentModels
    {
        public class ListPersonDocumentInput
        {
            public int PersonId { get; set; }
            public PersonType PersonType { get; set; }
            public int Id { get; set; }
            public DocumentType Type { get; set; }
            public string Value { get; set; } = string.Empty;
        }

        public class CreatePersonDocumentInput
        {
            public int PersonId { get; set; }
            public DocumentType Type { get; set; }
            public string Value { get; set; } = string.Empty;
        }

        public class ChangePersonDocumentInput
        {
            public int PersonId { get; set; }
            public int Id { get; set; }
            public DocumentType Type { get; set; }
            public string Value { get; set; } = string.Empty;
        }
    }
}
