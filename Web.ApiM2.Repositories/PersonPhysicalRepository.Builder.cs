using Dapper;
using System.Data;
using static Web.ApiM2.Repositories.Rules.PersonRules;
using Shared.Extensions;

namespace Web.ApiM2.Repositories;

public partial class PersonPhysicalRepository
{
    protected DynamicParameters CreateParameter(CreatePersonRule business)
    {
        var parameters = new DynamicParameters();

        parameters.Add(name: "@NAME", value: business.Input.Name, direction: ParameterDirection.Input);
        parameters.Add(name: "@CALLNAME", value: business.Input.CallName, direction: ParameterDirection.Input);
        parameters.Add(name: "@PERSONTYPE", value: business.Input.PersonType.ToInt32(), direction: ParameterDirection.Input);
        parameters.Add(name: "@CREATEDAT", value: business.Input.CreatedAt, direction: ParameterDirection.Input);
        parameters.Add(name: "@ENTERPRISEID", value: business.EnterpriseId, direction: ParameterDirection.Input);

        return parameters;
    }

    protected void CreateParameter(FindPersonRule business, string baseQuery, out DynamicParameters parameters, out string querySql)
    {
        parameters = new DynamicParameters();
        List<string> parts = new List<string>();

        if (business.Input == null) throw new Exception("BAD_FIND_INPUT");

        if (business.Input.Id > 0)
        {
            parts.Add(" P.PERSONID = @PERSONID ");
            parameters.Add(name: "@PERSONID", value: business.Input.Id, direction: ParameterDirection.Input);
        }

        if (string.IsNullOrEmpty(business.Input.Name) == false)
        {
            parts.Add(" UPPER(P.NAME) LIKE UPPER(@NAME) ");
            parameters.Add(name: "@NAME", value: $"%{business.Input.Name.Replace(" ", "%")}%", direction: ParameterDirection.Input);
        }
        
        if (string.IsNullOrEmpty(business.Input.CallName) == false)
        {
            parts.Add(" UPPER(P.CALLNAME) LIKE UPPER(@CALLNAME) ");
            parameters.Add(name: "@CALLNAME", value: $"'%{business.Input.CallName.Replace(" ", "%")}%'", direction: ParameterDirection.Input);
        }

        if (business.Input.PersonType.ToInt32() > 0)
        {
            parts.Add(" P.PERSONTYPE = @PERSONTYPE ");
            parameters.Add(name: "@PERSONTYPE", value: business.Input.PersonType.ToInt32(), direction: ParameterDirection.Input);
        }

        if (business.Input.CreatedAt.HasValue)
        {
            parts.Add(" TRUNC(P.CREATEDATE) = TRUNC(@CREATEDAT) ");
            parameters.Add(name: "@CREATEDAT", value: business.Input.CreatedAt, direction: ParameterDirection.Input);
        }

        parameters.Add(name: "@ENTERPRISEID", value: business.EnterpriseId, direction: ParameterDirection.Input);
        querySql = string.Format(baseQuery, parts.Any() ? $" AND {(string.Join(" AND ", parts.ToArray()))} " : string.Empty);
    }
}
