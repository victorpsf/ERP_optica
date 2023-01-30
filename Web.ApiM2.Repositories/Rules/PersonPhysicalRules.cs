using Web.ApiM2.Controller.Models;

namespace Web.ApiM2.Repositories.Rules
{
    public static class PersonRules
    {
        public class FindPersonRule
        {
            public PersonModels.FindPerson Input { get; set; } = new PersonModels.FindPerson();
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }

        public class CreatePersonRule
        {
            public PersonModels.CreatePerson Input { get; set; } = new PersonModels.CreatePerson();
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }
    }
}
