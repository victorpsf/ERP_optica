using Web.ApiM2.Controller.Models;

namespace Web.ApiM2.Repositories.Rules
{
    public static class PersonPhysicalRules
    {
        public class FindPersonPhysicalRule
        {
            public int Input { get; set; }
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }

        public class CreatePersonPhysicalRule
        {
            public PersonPhysicalModels.CreatePersonPhysical Input { get; set; } = new PersonPhysicalModels.CreatePersonPhysical();
            public int UserId { get; set; }
            public int EnterpriseId { get; set; }
        }
    }
}
