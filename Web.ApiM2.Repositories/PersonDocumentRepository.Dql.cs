using Dapper;
using static Web.ApiM2.Repositories.Rules.PersonDocumentRules;
using System.Data;
using static Application.Library.DocumentModels;
using static Application.Library.DatabaseModels;
using Shared.Extensions;
using Serilog;

namespace Web.ApiM2.Repositories;

public partial class PersonDocumentRepository
{
    public List<DocumentDto> Find(FindPersonDocumentRule business)
    {
        this.CreateParameter(business, out DynamicParameters parameters, out string Sql);
        List<DocumentDto> result = new List<DocumentDto>();

        this.Factory.Connect();
        try
        {
            var data = this.Factory.ExecuteReader<DocumentDto>(new BancoArgument
            {
                Sql = Sql,
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
