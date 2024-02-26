using Application.Base.Models;
using Application.Dtos;
using Application.Interfaces.Connections;
using Application.Services;
using Personal.Service.Repositories.Rules;
using static Personal.Service.Controllers.Models.DocumentModels;

namespace Personal.Service.Repositories.Services;

public class DocumentRepoService: BaseRepoService<IPersonalDatabase>
{
    private readonly DocumentRepository repository;

    public DocumentRepoService (IPersonalDatabase db): base(db)
    { this.repository = new DocumentRepository(db); }

    public PaginationOutput<DocumentInput, DocumentDtos.Document> Get(DocumentRules.FindDocumentWithPaginationRule rule)
    {
        var pagination = this.ExecuteQuery(
            this.repository.Pagination,
            rule,
            false
        );

        var documents = this.ExecuteQuery(
            this.repository.ListPerPagination,
            rule,
            false
        );

        return new PaginationOutput<DocumentInput, DocumentDtos.Document>
        {
            Total = pagination?.Total ?? 0,
            Page = pagination?.Page ?? 0,
            PerPage = pagination?.PerPage ?? 0,
            TotalPages = pagination?.TotalPages ?? 0,
            Values = documents ?? new List<DocumentDtos.Document>(),
            Search = rule.Search
        };
    }
}
