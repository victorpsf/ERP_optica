using Web.ApiM1.Controller.Models;

namespace Web.ApiM1.Repositories.Rules
{
    public static class EnterpriseRules
    {
        public class GetEnteprisesRule
        {
            public EnterpriseModels.ListEnterprisesInput Input { get; set; } = new EnterpriseModels.ListEnterprisesInput();
        }
    }
}
