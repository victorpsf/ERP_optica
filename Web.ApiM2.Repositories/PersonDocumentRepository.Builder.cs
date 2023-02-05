using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using Shared.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ApiM2.Repositories.Rules.PersonDocumentRules;
using static Application.Library.DocumentModels;

namespace Web.ApiM2.Repositories;

public partial class PersonDocumentRepository
{
    protected DynamicParameters CreateParameter(CreatePersonDocumentRule business)
    {
        var parameters = new DynamicParameters();

        parameters.Add(name: "@DOCUMENTTYPE", value: business.Input.Type.ToInt32(), direction: ParameterDirection.Input);
        parameters.Add(name: "@DOCUMENTVALUE", value: business.Input.Value, direction: ParameterDirection.Input);
        parameters.Add(name: "@PERSONID", value: business.Input.PersonId, direction: ParameterDirection.Input);
        parameters.Add(name: "@PERSONTYPE", value: business.PersonType.ToInt32(), direction: ParameterDirection.Input);

        return parameters;
    }

    protected DynamicParameters CreateParameter(ChangePersonDocumentRule business)
    {
        var parameters = this.CreateParameter(new CreatePersonDocumentRule
        {
            EnterpriseId = business.EnterpriseId,
            UserId = business.UserId,
            PersonType = business.PersonType,
            Input = business.Input,
        });

        parameters.Add(name: "@DOCUMENTID", value: business.Input.Id, direction: ParameterDirection.Input);
        return parameters;
    }

    protected void CreateParameter(FindPersonDocumentRule business, out DynamicParameters parameters, out string Sql)
    {
        var parts = new List<string>();
        parameters = new DynamicParameters();

        parameters.Add(name: "@PERSONID", value: business.Input.PersonId, direction: ParameterDirection.Input);
        parameters.Add(name: "@PERSONTYPE", value: business.Input.PersonType.ToInt32(), direction: ParameterDirection.Input);
        parameters.Add(name: "@ENTERPRISEID", value: business.EnterpriseId, direction: ParameterDirection.Input);

        if (business.Input.Type != DocumentType.Undefined)
        {
            parameters.Add(name: "@DOCUMENTTYPE", value: business.Input.Type.ToInt32(), direction: ParameterDirection.Input);
            parts.Add(" `PD`.`DOCUMENTTYPE` = @DOCUMENTTYPE ");
        }

        if (business.Input.Id > 0)
        {
            parameters.Add(name: "@DOCUMENTID", value: business.Input.Id, direction: ParameterDirection.Input);
            parts.Add(" `PD`.`DOCUMENTID` = @DOCUMENTID ");
        }

        if (!string.IsNullOrEmpty(business.Input.Value))
        {
            parameters.Add(name: "@DOCUMENTVALUE", value: business.Input.Value, direction: ParameterDirection.Input);
            parts.Add(" `PD`.`VALUE` = @DOCUMENTVALUE ");
        }

        Sql = string.Format(FindDocumentSql, (parts.Any()) ? $" AND {(string.Join(" AND ", parts.ToArray()))}": string.Empty);
    }
}
