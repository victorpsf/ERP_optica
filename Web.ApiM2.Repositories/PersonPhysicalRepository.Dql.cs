using Application.Library;
using Dapper;
using Serilog;
using System.Collections.Generic;
using System.Data;
using static Web.ApiM2.Repositories.Rules.PersonRules;

namespace Web.ApiM2.Repositories
{
    public partial class PersonPhysicalRepository
    {
        public List<PersonModels.PersonDto> Find(FindPersonRule business)
        {
            this.CreateParameter(business, FindPersonPhysicalSql, out DynamicParameters parameters, out string QuerySql);
            var result = new List<PersonModels.PersonDto> ();

            this.Factory.Connect();
            try
            {
                result = this.Factory.ExecuteReader<PersonModels.PersonDto>(new DatabaseModels.BancoArgument
                {
                    Sql = QuerySql,
                    Parameter = parameters
                }).ToList();
            }

            catch (Exception ex)
            { Log.Error(string.Format("PersonPhysicalRepository.Find :: {0}", ex.Message)); }

            this.Factory.Disconnect();

            return result;
        }
    }
}
