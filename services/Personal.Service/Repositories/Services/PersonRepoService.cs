using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces.Connections;
using Application.Services;
using Personal.Service.Repositories.Rules;

namespace Personal.Service.Repositories.Services;

public class PersonRepoService: BaseRepoService<IPersonalDatabase>
{
    private PersonRepository personRepository;

    public PersonRepoService(IPersonalDatabase db): base(db) 
    { this.personRepository = new PersonRepository(db); }

    public PersonDtos.PersonPhysical? Create(PersonRules.CreatePersonRule rule)
    {
        PersonDtos.PersonPhysical? person = null;

        try
        { this.db.Connect(); }

        catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        {
            person = this.personRepository.Save(
                PersonRules.PersistPersonPhysicalRule.ByCreatePersonRule(rule)
            );

            if (person.Id == 0) 
                throw new Exception("Person don't perssisted");

            this.db.ControlData(new DatabaseModels.BancoCommitArgument<int>
            {
                Control = DatabaseModels.DmlType.Insert,
                EnterpriseId = rule.EnterpriseId,
                UserId = rule.UserId,
                Entity = DatabaseModels.EntityType.PersonPhysical,
                EntityId = person.Id
            });
            this.db.Commit();
        }

        catch (Exception ex)
        {
            this.db.Rollback();
            this.db.Disconnect();
            throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex);
        }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        return person;
    }
}
