using Personal.Service.Repositories.Queries;
using Personal.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;
using System.Data;
using Application.Extensions;
using static Application.Dtos.PersonDtos;

namespace Personal.Service.Repositories.Services;

public partial class PersonRepoService
{
    public void Build(PersonRules.FindPersonPhysicalRule rule, out ParameterCollection Parameters, out string Sql)
    {
        Parameters = new ParameterCollection();
        Sql = PersonQuery.BaseFindPersonSql;
        List<string> where = new List<string>();

        where.Add("`P`.`PERSONTYPE` = @PERSONTYPE");
        Parameters.Add("@PERSONTYPE", PersonType.Physical.intValue(), ParameterDirection.Input);

        if (rule.Input.Id > 0)
        {
            where.Add("`P`.`PERSONID` = @PERSONID");
            Parameters.Add("@PERSONID", rule.Input.Id, ParameterDirection.Input);
        }

        if (!string.IsNullOrEmpty(rule.Input.Name))
        {
            where.Add("`P`.`NAME` = @NAME");
            Parameters.Add("@NAME", rule.Input.Name.ToLikeSql(), ParameterDirection.Input);
        }

        if (!string.IsNullOrEmpty(rule.Input.CallName))
        {
            where.Add("`P`.`CALLNAME` = @CALLNAME");
            Parameters.Add("@CALLNAME", rule.Input.CallName.ToLikeSql(), ParameterDirection.Input);
        }

        if (rule.Input.BirthDate.InRange(130, 0))
        {
            where.Add("`P`.`CREATEDATE` = @CREATEDATE");
            Parameters.Add("@CREATEDATE", rule.Input.BirthDate, ParameterDirection.Input);
        }

        Sql += $"\tAND {string.Join("\n\tAND ", where.ToArray())}";
    }
}
