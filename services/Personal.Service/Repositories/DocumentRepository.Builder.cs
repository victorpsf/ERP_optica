using Application.Extensions;
using Personal.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;

namespace Personal.Service.Repositories;

public partial class DocumentRepository
{
    private void Calculate(DocumentRules.FindDocumentWithPaginationRule rule, bool calculateOffset, out long Limit, out long Offset)
    {
        if (calculateOffset)
        {
            Limit = rule.Input.PerPage > 0 ? rule.Input.PerPage : 20;
            Offset = (rule.Input.Page == 0) ? rule.Input.Page : Limit * rule.Input.Page;
        }

        else
        {
            Limit = rule.Input.PerPage;
            Offset = rule.Input.Page;
        }
    }

    private void Build(DocumentRules.FindDocumentWithPaginationRule rule, bool calculateOffset, out ParameterCollection Parameters, out string ComplementQuery, out string ComplementSubQuery)
    {
        Parameters = ParameterCollection.GetInstance();
        var whereQuery = new List<string>();
        var whereSubQuery = new List<string>();
        this.Calculate(rule, calculateOffset, out long  Limit, out long Offset);

        Parameters.Add("@ENTERPRISEID", rule.EnterpriseId);
        Parameters.Add("@LIMIT", Limit);
        Parameters.Add("@OFFSET", Offset);

        whereSubQuery.Add("P.ENTERPRISEID = @ENTERPRISEID");

        //if (rule.Search.PersonId > 0)
        //{
        //    Parameters.Add("@PERSONID", rule.Search.PersonId);
        //    whereQuery.Add("PD.PERSONID = @PERSONID");
        //}

        if (!string.IsNullOrEmpty(rule.Search.Value))
        {
            if (rule.IntelligentSearch)
                Parameters.Add("@VALUE", $"%{rule.Search.Value}%");
            else
                Parameters.Add("@VALUE", rule.Search.Value);
            whereQuery.Add("PD.VALUE = @VALUE");
        }

        if (rule.Search.DocumentType.intValue() > 0)
        {
            Parameters.Add("@DOCUMENTTYPE", rule.Search.DocumentType.intValue());
            whereQuery.Add("PD.DOCUMENTTYPE = @DOCUMENTTYPE");
        }

        ComplementQuery = whereQuery.Any() ? $"AND {string.Join(" AND ", whereQuery.ToArray())}": "";
        ComplementSubQuery = string.Join(" AND ", whereSubQuery.ToArray());
    }
}
