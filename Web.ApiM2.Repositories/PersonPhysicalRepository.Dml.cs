using static Application.Library.PersonModels;
using static Web.ApiM2.Repositories.Rules.PersonRules;
using Serilog;
using Dapper;
using System.Data;
using static Application.Library.DatabaseModels;

namespace Web.ApiM2.Repositories;

public partial class PersonPhysicalRepository
{
    public int Save(CreatePersonRule business)
    {
        this.Factory.Connect();
        int id = 0;

        try
        {
            var parameters = this.CreateParameter(business);

            id = this.Factory.Execute<int>(new BancoExecuteArgument
            {
                Sql = CreatePersonPhysicalSql,
                Parameter = parameters,
                Output = "@PERSONID"
            });

            if (id > 0)
            {
                this.Factory.ControlData(new BancoCommitArgument
                {
                    Control = DmlType.Insert,
                    Entity = (EntityType) business.Input.PersonType,
                    EntityId = id,
                    EnterpriseId = business.EnterpriseId,
                    UserId = business.UserId
                });
                this.Factory.Commit();
            }

            else throw new Exception("PERSONID IS ZERO OR NULL");
        }

        catch (Exception ex)
        {
            this.Factory.Rollback();
            Log.Error(string.Format("PersonPhysicalRepository.Save :: {0}", ex.Message));
        }

        this.Factory.Disconnect();

        if (!(id > 0)) throw new Exception("PERSONID_IS_NULLABLE_OR_ZERO");
        return id;
    }
}
