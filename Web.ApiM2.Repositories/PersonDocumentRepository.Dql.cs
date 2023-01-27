using Dapper;
using static Web.ApiM2.Repositories.Rules.PersonDocumentRules;
using System.Data;
using static Application.Library.DocumentModels;
using static Application.Library.DatabaseModels;
using Shared.Extensions;
using Serilog;

namespace Web.ApiM2.Repositories
{
    public partial class PersonDocumentRepository
    {
        public List<DocumentDto> Find(FindPersonDocumentRule business)
        {
            List<DocumentDto> result = new List<DocumentDto>();
            var parameters = new DynamicParameters();
            var parts = new List<string>();

            parameters.Add(name: "@PERSONID", value: business.Input.PersonId, direction: ParameterDirection.Input);
            parameters.Add(name: "@PERSONTYPE", value: business.Input.PersonType.ToInt32(), direction: ParameterDirection.Input);
            parameters.Add(name: "@ENTERPRISEID", value: business.EnterpriseId, direction: ParameterDirection.Input);

            if (business.Input.Type != DocumentType.Undefined) {
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

            this.Factory.Connect();
            try
            {
                var data = this.Factory.ExecuteReader<DocumentDto>(new BancoArgument
                {
                    Sql = string.Format(FindDocumentSql, (!parts.Any()) ? string.Empty: $" AND {(string.Join(" AND ", parts.ToArray()))}" ),
                    Parameter = parameters
                });
                result.AddRange(data.ToList());
            }

            catch (Exception ex)
            {
                Log.Error(string.Format("PersonDocumentRepository.Find :: {0}", ex.Message));
            }
            this.Factory.Disconnect();

            return result;
        }
    }
}
