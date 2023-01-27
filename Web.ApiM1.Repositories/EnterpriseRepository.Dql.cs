using Application.Library;
using Dapper;
using Serilog;
using System.Data;
using static Web.ApiM1.Repositories.Rules.EnterpriseRules;

namespace Web.ApiM1.Repositories
{
    public partial class EnterpriseRepository
    {
        public List<EnterpriseModels.EnterpriseDto> Get(GetEnteprisesRule business)
        {
            var parameters = new DynamicParameters();
            var parts = new List<string>();
            var result = new List<EnterpriseModels.EnterpriseDto>();

            if (business.Input.Id > 0)
            {
                parameters.Add(name: "@ID", business.Input.Id, direction: ParameterDirection.Input);
                parts.Add(" `E`.`ENTERPRISEID` = @ID ");
            }

            if (!string.IsNullOrEmpty(business.Input.Name))
            {
                parameters.Add(name: "@NAME", business.Input.Name, direction: ParameterDirection.Input);
                parts.Add(" UPPER(`E`.`NAME`) LIKE UPPER(CONCAT('%', CONCAT(@NAME, '%'))) ");
            }

            this.Factory.Connect();

            try
            {
                var data = this.Factory.ExecuteReader<EnterpriseModels.EnterpriseDto>(new DatabaseModels.BancoArgument
                {
                    Sql = string.Format(GetEnterprisesSql, (!parts.Any()) ? string.Empty : $"AND {(string.Join(" AND ", parts.ToArray()))}"),
                    Parameter = parameters
                });

                result.AddRange(data.ToList());
            }

            catch (Exception ex)
            {
                Log.Error(string.Format("EnterpriseRepository.Get :: {0}", ex.Message));
            }

            this.Factory.Disconnect();
            return result;
        }
    }
}
