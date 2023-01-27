using static Application.Library.PersonModels;
using static Web.ApiM2.Controller.Models.PersonDocumentModels;

namespace Web.ApiM2.Repositories.Rules
{
    public static class PersonDocumentRules
    {
        public class FindPersonDocumentRule
        {
            public ListPersonDocumentInput Input { get; set; } = new ListPersonDocumentInput();
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }

        public class CreatePersonDocumentRule
        {
            public CreatePersonDocumentInput Input { get; set; } = new CreatePersonDocumentInput();
            public PersonType PersonType { get; set; }
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }

        public class ChangePersonDocumentRule
        {
            public ChangePersonDocumentInput Input { get; set; } = new ChangePersonDocumentInput();
            public PersonType PersonType { get; set; }
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }
    }
}
