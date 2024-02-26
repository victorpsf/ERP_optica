using Application.Dtos;
using Application.Interfaces.Connections;
using Personal.Service.Repositories.Queries;
using Personal.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;
using static Application.Dtos.PersonDtos;

namespace Personal.Service.Repositories;

public partial class DocumentRepository
{
    private readonly IPersonalDatabase db;

    public DocumentRepository(IPersonalDatabase db)
    { this.db = db; }

    public PaginationDtos.PaginationDto? Pagination(DocumentRules.FindDocumentWithPaginationRule rule)
    {
        this.Build(rule, false, out ParameterCollection Parameters, out string ComplementQuery, out string ComplementSubQuery);

        return this.db.Find<PaginationDtos.PaginationDto>(new BancoArgument
        {
            Sql = string.Format(DocumentQuery.FindInfoPaginationSql, ComplementSubQuery, ComplementQuery),
            Parameter = Parameters
        });
    }

    public List<DocumentDtos.Document> ListPerPagination(DocumentRules.FindDocumentWithPaginationRule rule)
    {
        this.Build(rule, true, out ParameterCollection Parameters, out string ComplementQuery, out string ComplementSubQuery);

        return this.db.ExecuteReader<DocumentDtos.Document>(new BancoArgument
        {
            Sql = string.Format(DocumentQuery.FindDocumentUsingPagination, ComplementSubQuery, ComplementQuery),
            Parameter = Parameters
        }).ToList();
    }

    public List<DocumentDtos.Document> ListByPersonId(int personId)
        => this.ListByPersonIds(new List<int> { personId });

    public List<DocumentDtos.Document> ListByPersonIds(List<int> personIds)
        => this.db.ExecuteReader<DocumentDtos.Document>(new BancoArgument
        {
            Sql = DocumentQuery.FindDocumentByPersonId,
            Parameter = ParameterCollection.GetInstance()
                .Add("@PERSONIDS", personIds)
        }).ToList();
}
