using Application.Base.Models;
using static Personal.Service.Controllers.Models.DocumentModels;

namespace Personal.Service.Repositories.Rules;

public static class DocumentRules
{
    #region DQL
    public class FindDocumentWithPaginationRule
    {
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public bool IntelligentSearch { get; set; }
        public PaginationInput<DocumentInput> Input { get; set; } = new PaginationInput<DocumentInput>();
        public DocumentInput Search { get => this.Input.Search; }
    }
    #endregion
}
