using Application.Dtos;
using Application.Interfaces.Connections;
using Personal.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;
using System.Data;
using Personal.Service.Repositories.Queries;

namespace Personal.Service.Repositories;

public class PersonRepository: PersonQuery
{
    private readonly IPersonalDatabase db;
    
    public PersonRepository(IPersonalDatabase db)
    {
        this.db = db;
    }

    public PersonDtos.PersonPhysical Save(PersonRules.PersistPersonPhysicalRule rule)
    {
        var id = this.db.Execute<int>(new BancoExecuteScalarArgument
        {
            Output = "@PersonId",
            Sql = CreatePersonSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@NAME", rule.Name, ParameterDirection.Input)
                .Add("@CALLNAME", rule.CallName, ParameterDirection.Input)
                .Add("@PERSONTYPE", (int)rule.PersonType, ParameterDirection.Input)
                .Add("@CREATEDATE", rule.BirthDate, ParameterDirection.Input)
                .Add("@VERSION", rule.Version, ParameterDirection.Input)
                .Add("@ENTERPRISEID", rule.EnterpriseId, ParameterDirection.Input)
        });

        rule.Id = id;
        return rule.toDto();
    }
}
