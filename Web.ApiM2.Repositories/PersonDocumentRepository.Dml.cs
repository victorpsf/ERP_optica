using Dapper;
using Serilog;
using Shared.Extensions;
using static Web.ApiM2.Repositories.Rules.PersonDocumentRules;
using static Application.Library.DatabaseModels;
using System.Data;

namespace Web.ApiM2.Repositories;

public partial class PersonDocumentRepository
{
    public int Save(CreatePersonDocumentRule business)
    {
        this.ValidateRule(business);
        int documentId = 0;

        this.Factory.Connect();
        try
        {
            documentId = this.Factory.Execute<int>(new BancoExecuteArgument
            {
                Sql = CreateDocumentSql,
                Parameter = this.CreateParameter(business),
                Output = "@DOCUMENTID"
            });

            if (documentId > 0)
            {
                this.Factory.ControlData(new BancoCommitArgument
                {
                    EnterpriseId = business.EnterpriseId,
                    UserId = business.UserId,
                    Entity = EntityType.Document,
                    EntityId = documentId,
                    Control = DmlType.Insert
                });
                this.Factory.Commit();
            }

            else throw new Exception("ERRO_DOCUMENTID_IS_NULL_OR_ZERO");
        }

        catch (Exception ex)
        {
            Log.Error(string.Format("PersonDocumentRepository.Save :: {0}", ex.Message));
            this.Factory.Rollback();
        }
        this.Factory.Disconnect();

        if (!(documentId > 0)) throw new Exception("ERRO_DOCUMENTID_IS_NULL_OR_ZERO");

        return documentId;
    }

    public void Save(ChangePersonDocumentRule business)
    {
        this.ValidateRule(business);


        this.Factory.Connect();
        try
        {
            this.Factory.Execute(new BancoArgument
            {
                Sql = ChangeDocumentSql,
                Parameter = this.CreateParameter(business)
            });
            this.Factory.ControlData(new BancoCommitArgument
            {
                Control = DmlType.Update,
                Entity = EntityType.Document,
                EntityId = business.Input.Id,
                EnterpriseId = business.EnterpriseId,
                UserId = business.UserId
            });
            this.Factory.Commit();
        }

        catch (Exception ex)
        {
            Log.Error(string.Format("PersonDocumentRepository.Save :: {0}", ex.Message));
            this.Factory.Rollback();
        }
        this.Factory.Disconnect();
    }
}
