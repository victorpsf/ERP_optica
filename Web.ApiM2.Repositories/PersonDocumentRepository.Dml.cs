using Dapper;
using Serilog;
using Shared.Extensions;
using static Web.ApiM2.Repositories.Rules.PersonDocumentRules;
using static Application.Library.DatabaseModels;
using System.Data;

namespace Web.ApiM2.Repositories
{
    public partial class PersonDocumentRepository
    {
        public int Save(CreatePersonDocumentRule business)
        {
            int result = 0;

            var documents = this.Find(new FindPersonDocumentRule {
                EnterpriseId = business.EnterpriseId,
                UserId = business.UserId,
                Input = new Controller.Models.PersonDocumentModels.ListPersonDocumentInput
                {
                    PersonType = business.PersonType,
                    PersonId = business.Input.PersonId,
                    Type = business.Input.Type
                }
            });

            if (documents.Any())
            {
                var document = documents.First();

                this.Save(new ChangePersonDocumentRule
                {
                    Input = new Controller.Models.PersonDocumentModels.ChangePersonDocumentInput
                    {
                        Id = document.Id,
                        Type = document.Type,
                        PersonId = business.Input.PersonId,
                        Value = business.Input.Value
                    },
                    EnterpriseId = business.EnterpriseId,
                    UserId = business.UserId,
                    PersonType = business.PersonType
                });

                return document.Id;
            }

            var parameters = new DynamicParameters();

            parameters.Add(name: "@DOCUMENTTYPE", value: business.Input.Type.ToInt32(), direction: ParameterDirection.Input);
            parameters.Add(name: "@DOCUMENTVALUE", value: business.Input.Value, direction: ParameterDirection.Input);
            parameters.Add(name: "@PERSONID", value: business.Input.PersonId, direction: ParameterDirection.Input);
            parameters.Add(name: "@PERSONTYPE", value: business.PersonType.ToInt32(), direction: ParameterDirection.Input);

            this.Factory.Connect();
            try
            {
                result = this.Factory.Execute<int>(new BancoExecuteArgument
                {
                    Sql = CreateDocumentSql,
                    Parameter = parameters,
                    Output = "@DOCUMENTID"
                });

                if (result > 0)
                {
                    this.Factory.ControlData(new BancoCommitArgument
                    {
                        EnterpriseId = business.EnterpriseId,
                        UserId = business.UserId,
                        Entity = EntityType.Document,
                        EntityId = result,
                        Control = DmlType.Insert
                    });
                    this.Factory.Commit();
                }

                else throw new Exception("DOCUMENTID IS NULL OR ZERO");
            }

            catch (Exception ex)
            {
                Log.Error(string.Format("PersonDocumentRepository.Save :: {0}", ex.Message));
                this.Factory.Rollback();
            }
            this.Factory.Disconnect();

            if (!(result > 0)) throw new Exception("DOCUMENTID_IS_NULL_OR_ZERO");

            return result;
        }

        public void Save(ChangePersonDocumentRule business)
        {
            var parameters = new DynamicParameters();

            parameters.Add(name: "@DOCUMENTID", value: business.Input.Id, direction: ParameterDirection.Input);
            parameters.Add(name: "@DOCUMENTTYPE", value: business.Input.Type.ToInt32(), direction: ParameterDirection.Input);
            parameters.Add(name: "@DOCUMENTVALUE", value: business.Input.Value, direction: ParameterDirection.Input);
            parameters.Add(name: "@PERSONID", value: business.Input.PersonId, direction: ParameterDirection.Input);
            parameters.Add(name: "@PERSONTYPE", value: business.PersonType.ToInt32(), direction: ParameterDirection.Input);

            this.Factory.Connect();
            try
            {
                this.Factory.Execute(new BancoArgument
                {
                    Sql = ChangeDocumentSql,
                    Parameter = parameters
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
}
