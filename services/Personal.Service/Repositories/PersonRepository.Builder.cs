using Application.Dtos;
using Application.Extensions;
using Personal.Service.Repositories.Rules;
using System.Data;
using static Application.Base.Models.DatabaseModels;

namespace Personal.Service.Repositories;

public partial class PersonRepository
{
    private void Calculate(PersonRules.FindPersonPhysicalWithPaginationRule rule, out long Limit, out long Offset)
    {
        Limit = rule.Input.PerPage > 0 ? rule.Input.PerPage : 20;
        Offset = (rule.Input.Page == 0) ? rule.Input.Page : Limit * rule.Input.Page;
    }

    private void Calculate(PersonRules.FindPersonJuridicalWithPaginationRule rule, out long Limit, out long Offset)
    {
        Limit = rule.Input.PerPage > 0 ? rule.Input.PerPage : 20;
        Offset = (rule.Input.Page == 0) ? rule.Input.Page : Limit * rule.Input.Page;
    }

    private void Build(PersonRules.FindPersonPhysicalWithPaginationRule rule, bool calculateOffset, out ParameterCollection Parameters, out string Complement)
    {
        Parameters = new ParameterCollection();
        var where = new List<string>();
        this.Calculate(rule, out long Limit, out long Offset);

        Parameters.Add("@ENTERPRISEID", rule.EnterpriseId);
        Parameters.Add("@PERSONTYPE", PersonDtos.PersonType.Physical.intValue());
        Parameters.Add("@LIMIT", Limit);
        Parameters.Add("@OFFSET", Offset);

        where.Add("P.ENTERPRISEID = @ENTERPRISEID");
        where.Add("P.PERSONTYPE = @PERSONTYPE");

        if (rule.Search.Id > 0)
        {
            Parameters.Add("@ID", rule.Search.Id);
            where.Add("P.PERSONID = @ID");
        }

        if (!string.IsNullOrEmpty(rule.Search.Name))
        {
            if (rule.IntelligentSearch)
                Parameters.Add("@NAME", $"%{rule.Search.Name}%");
            else
                Parameters.Add("@NAME", rule.Search.Name);
            where.Add("P.NAME = @NAME");
        }

        if (!string.IsNullOrEmpty(rule.Search.CallName))
        {
            if (rule.IntelligentSearch)
                Parameters.Add("@CALLNAME", $"%{rule.Search.CallName}%");
            else
                Parameters.Add("@CALLNAME", rule.Search.CallName);
            where.Add("P.CALLNAME = @CALLNAME");
        }

        if (rule.Search.BirthDate.InRange(-120, 0))
        {
            Parameters.Add("@CREATEDATE", rule.Search.BirthDate);
            where.Add("P.CREATEDATE = @CREATEDATE");
        }

        Complement = string.Join(" AND ", where.ToArray());
    }

    private void Build(PersonRules.FindPersonJuridicalWithPaginationRule rule, bool calculateOffset, out ParameterCollection Parameters, out string Complement)
    {
        Parameters = new ParameterCollection();
        var where = new List<string>();
        this.Calculate(rule, out long Limit, out long Offset);

        Parameters.Add("@ENTERPRISEID", rule.EnterpriseId);
        Parameters.Add("@PERSONTYPE", PersonDtos.PersonType.Juridical.intValue());
        Parameters.Add("@LIMIT", Limit);
        Parameters.Add("@OFFSET", Offset);

        where.Add("P.ENTERPRISEID = @ENTERPRISEID");
        where.Add("P.PERSONTYPE = @PERSONTYPE");

        if (rule.Search.Id > 0)
        {
            Parameters.Add("@ID", rule.Search.Id);
            where.Add("P.PERSONID = @ID");
        }

        if (!string.IsNullOrEmpty(rule.Search.Name))
        {
            if (rule.IntelligentSearch)
                Parameters.Add("@NAME", $"%{rule.Search.Name}%");
            else
                Parameters.Add("@NAME", rule.Search.Name);
            where.Add("P.NAME = @NAME");
        }

        if (!string.IsNullOrEmpty(rule.Search.CallName))
        {
            if (rule.IntelligentSearch)
                Parameters.Add("@CALLNAME", $"%{rule.Search.CallName}%");
            else
                Parameters.Add("@CALLNAME", rule.Search.CallName);
            where.Add("P.CALLNAME = @CALLNAME");
        }

        if (rule.Search.Fundation.InRange(-240, 0))
        {
            Parameters.Add("@CREATEDATE", rule.Search.Fundation);
            where.Add("P.CREATEDATE = @CREATEDATE");
        }

        Complement = string.Join(" AND ", where.ToArray());
    }
}
