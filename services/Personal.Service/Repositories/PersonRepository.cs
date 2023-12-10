using Application.Dtos;
using Application.Interfaces.Connections;
using Personal.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;
using System.Data;
using Personal.Service.Repositories.Queries;
using static Application.Dtos.PersonDtos;
using Application.Extensions;
using Application.Exceptions;
using Application.Base.Models;

namespace Personal.Service.Repositories;

public class PersonRepository: PersonQuery
{
    private readonly IPersonalDatabase db;
    
    public PersonRepository(IPersonalDatabase db)
    {
        this.db = db;
    }

    public List<PersonPhysical> FindByQuery(string Sql, ParameterCollection Parameters)
        => this.db.ExecuteReader<PersonPhysical>(new BancoArgument
        {
            Sql = Sql,
            Parameter = Parameters
        }).ToList();

    public PersonPhysical? FindById(int personId)
        => this.db.Find<PersonPhysical>(new BancoArgument
        {
            Sql = FindByIdSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@PERSONID", personId, ParameterDirection.Input)
        });

    public PersonPhysical? Save(PersonRules.PersistPersonPhysicalRule rule)
    {
        if (rule.Input.Id > 0)
        {
            var person = this.FindById(rule.Input.Id);

            if (person is null || person.Version != rule.Input.Version)
                throw new Exception("ERROR_DB_EXECUTION_FAILED");

            this.db.Execute(new BancoExecuteArgument
            {
                Sql = ChangePersonSql,
                Parameter = ParameterCollection.GetInstance()
                    .Add("@NAME", rule.Input.Name, ParameterDirection.Input)
                    .Add("@CALLNAME", rule.Input.CallName, ParameterDirection.Input)
                    .Add("@CREATEDATE", rule.Input.BirthDate, ParameterDirection.Input)
                    .Add("@ENTERPRISEID", rule.EnterpriseId, ParameterDirection.Input)
                    .Add("@VERSION", (person.Version + 1), ParameterDirection.Input)
                    .Add("@PERSONID", person.Id, ParameterDirection.Input)
            });

            this.db.ControlData(new BancoCommitArgument<int>
            {
                Control = DmlType.Update,
                EnterpriseId = rule.EnterpriseId,
                UserId = rule.UserId,
                Entity = EntityType.PersonPhysical,
                EntityId = person.Id
            });

            return this.FindById(person.Id);
        }

        else
        {
            int personId = this.db.Execute<int>(new BancoExecuteScalarArgument
            {
                Output = "@PersonId",
                Sql = CreatePersonSql,
                Parameter = ParameterCollection.GetInstance()
                    .Add("@NAME", rule.Input.Name, ParameterDirection.Input)
                    .Add("@CALLNAME", rule.Input.CallName, ParameterDirection.Input)
                    .Add("@PERSONTYPE", PersonType.Physical.intValue(), ParameterDirection.Input)
                    .Add("@CREATEDATE", rule.Input.BirthDate, ParameterDirection.Input)
                    .Add("@VERSION", 0, ParameterDirection.Input)
                    .Add("@ENTERPRISEID", rule.EnterpriseId, ParameterDirection.Input)
            });

            if (personId == 0)
                throw new Exception("Person don't perssisted");

            this.db.ControlData(new BancoCommitArgument<int>
            {
                Control = DmlType.Insert,
                EnterpriseId = rule.EnterpriseId,
                UserId = rule.UserId,
                Entity = EntityType.PersonPhysical,
                EntityId = personId
            });

            return this.FindById(personId);
        }
    }
}
