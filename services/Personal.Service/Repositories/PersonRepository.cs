using Application.Interfaces.Connections;
using Personal.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;
using System.Data;
using Personal.Service.Repositories.Queries;
using static Application.Dtos.PersonDtos;
using Application.Extensions;
using Application.Dtos;

namespace Personal.Service.Repositories;

public partial class PersonRepository: PersonQuery
{
    private readonly IPersonalDatabase db;
    
    public PersonRepository(IPersonalDatabase db)
    {
        this.db = db;
    }

    public PaginationDtos.PaginationDto? Pagination(PersonRules.FindPersonPhysicalWithPaginationRule rule)
    {
        this.Build(rule, false, out ParameterCollection Parameters, out string Complement);

        return this.db.Find<PaginationDtos.PaginationDto>(new BancoArgument
        {
            Sql = string.Format(PersonPhysicalQuery.FindInfoPaginationSql, Complement),
            Parameter = Parameters
        });
    }

    public List<PersonPhysical> ListPerPagination(PersonRules.FindPersonPhysicalWithPaginationRule rule)
    {
        this.Build(rule, true, out ParameterCollection Parameters, out string Complement);

        return this.db.ExecuteReader<PersonPhysical>(new BancoArgument
        {
            Sql = string.Format(PersonPhysicalQuery.FindPersonUsingPagination, Complement),
            Parameter = Parameters
        }).ToList();
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

    public PersonPhysical? Save(PersonRules.PersistPersonPhysicalDtoRule rule)
    {
        if (rule.Input is null)
            throw new ArgumentNullException(nameof(rule));

        var parameters = ParameterCollection.GetInstance();

        if (rule.Input.Id > 0)
            parameters.Add("@PERSONID", rule.Input.Id);
        parameters.Add("@NAME", rule.Input.Name, ParameterDirection.Input);
        parameters.Add("@PERSONTYPE", rule.Input.PersonType.intValue());
        parameters.Add("@CALLNAME", rule.Input.CallName, ParameterDirection.Input);
        parameters.Add("@CREATEDATE", rule.Input.BirthDate, ParameterDirection.Input);
        parameters.Add("@ENTERPRISEID", rule.Input.EnterpriseId, ParameterDirection.Input);
        parameters.Add("@VERSION", rule.Input.Version, ParameterDirection.Input);

        if (rule.Input.Id > 0)
        {
            this.db.Execute(new BancoExecuteArgument
            {
                Sql = PersonPhysicalQuery.ChangePersonSql,
                Parameter = parameters
            });

            this.db.ControlData(new BancoCommitArgument<int>
            {
                Control = DmlType.Update,
                EnterpriseId = rule.EnterpriseId,
                UserId = rule.UserId,
                Entity = EntityType.PersonPhysical,
                EntityId = rule.Input.Id
            });
        }

        else
        {
            int personId = this.db.Execute<int>(new BancoExecuteScalarArgument
            {
                Output = "@PersonId",
                Sql = PersonPhysicalQuery.CreatePersonSql,
                Parameter = parameters
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
            rule.Input.Id = personId;
        }

        return rule.Input;
    }
}
