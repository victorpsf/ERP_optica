using Application.Library;
using Dapper;
using Serilog;
using System.Data;
using static Web.ApiM2.Repositories.Rules.PersonPhysicalRules;

namespace Web.ApiM2.Repositories
{
    public partial class PersonPhysicalRepository
    {
        public PersonModels.PersonPhysicalDto? Find(FindPersonPhysicalRule business)
        {
            PersonModels.PersonPhysicalDto? result = null;
            var parameters = new DynamicParameters();

            parameters.Add(name: "@PERSONID", value: business.Input, direction: ParameterDirection.Input);
            parameters.Add(name: "@ENTERPRISEID", value: business.EnterpriseId, direction: ParameterDirection.Input);

            this.Factory.Connect();
            try
            {
                result = this.Factory.Find<PersonModels.PersonPhysicalDto>(new DatabaseModels.BancoArgument
                {
                    Sql = FindPersonPhysicalSql,
                    Parameter = parameters
                });
            }

            catch (Exception ex)
            { Log.Error(string.Format("PersonPhysicalRepository.Find :: {0}", ex.Message)); }

            this.Factory.Disconnect();

            return result;
        }
    }
}
