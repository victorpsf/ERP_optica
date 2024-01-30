using Application.Base.Models;
using Application.Exceptions;
using Application.Interfaces.Connections;
using Application.Services;
using Personal.Service.Repositories.Rules;
using static Application.Dtos.PersonDtos;
using static Personal.Service.Controllers.Models.PersonModels;

namespace Personal.Service.Repositories.Services;


public partial class PersonRepoService: BaseRepoService<IPersonalDatabase>
{
    private PersonRepository personRepository;

    public PersonRepoService(IPersonalDatabase db): base(db) 
    { this.personRepository = new PersonRepository(db); }

    public PaginationOutput<PersonPhysicalInput, PersonPhysical> Get(PersonRules.FindPersonPhysicalWithPaginationRule rule)
    {
        var pagination = this.ExecuteQuery(
            this.personRepository.Pagination,
            rule,
            false
        );

        var persons = this.ExecuteQuery<List<PersonPhysical>, PersonRules.FindPersonPhysicalWithPaginationRule>(
            this.personRepository.ListPerPagination,
            rule,
            false
        );
        return new PaginationOutput<PersonPhysicalInput, PersonPhysical>
        {
            Total = pagination?.Total ?? 0,
            Page = pagination?.Page ?? 0,
            PerPage = pagination?.PerPage ?? 0,
            TotalPages = pagination?.TotalPages ?? 0,
            Values = persons ?? new List<PersonPhysical>(),
            Search = rule.Search
        };
    }

    public PaginationOutput<PersonJuridicalInput, PersonJuridical> Get(PersonRules.FindPersonJuridicalWithPaginationRule rule)
    {
        var pagination = this.ExecuteQuery(
            this.personRepository.Pagination,
            rule,
            false
        );

        var persons = this.ExecuteQuery<List<PersonJuridical>, PersonRules.FindPersonJuridicalWithPaginationRule>(
            this.personRepository.ListPerPagination,
            rule,
            false
        );
        return new PaginationOutput<PersonJuridicalInput, PersonJuridical>
        {
            Total = pagination?.Total ?? 0,
            Page = pagination?.Page ?? 0,
            PerPage = pagination?.PerPage ?? 0,
            TotalPages = pagination?.TotalPages ?? 0,
            Values = persons ?? new List<PersonJuridical>(),
            Search = rule.Search
        };
    }

    public PersonPhysical? Save(PersonRules.PersistPersonPhysicalRule rule)
    {
        PersonPhysical? person = rule.Input.Id > 0 ? this.ExecuteQuery(
            this.personRepository.FindPersonPhysicalById,
            rule.Input.Id,
            false
        ) : null;

        // [TODO]: adicionar stack de erro
        if (
            person is null ||
            (
                person is not null &&
                (
                    person.Version != rule.Input.Version ||
                    person.EnterpriseId != rule.EnterpriseId ||
                    person.PersonType != PersonType.Physical
                )
            )
        ) throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_PERSON_REQUIRED_DOCUMENT_NOT_INFORMED);

        return this.ExecuteQuery(
            this.personRepository.Save,
            new PersonRules.PersistPersonPhysicalDtoRule
            {
                EnterpriseId = rule.EnterpriseId,
                UserId = rule.UserId,
                Input = rule.ToDto(person),
            },
            true
        );
    }

    public PersonPhysical? Remove(PersonRules.RemovePersonPhysicalRule rule)
    {
        PersonPhysical? person = rule.Input.Id > 0 ? this.ExecuteQuery(
            this.personRepository.FindPersonPhysicalById,
            rule.Input.Id,
            false
        ) : null;

        // [TODO]: adicionar stack de erro
        if (
            person is null ||
            (
                person is not null &&
                (
                    person.Version != rule.Input.Version ||
                    person.EnterpriseId != rule.EnterpriseId ||
                    person.PersonType != PersonType.Physical
                )
            )
        ) throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_PERSON_REQUIRED_DOCUMENT_NOT_INFORMED);

        this.ExecuteQuery(
            this.personRepository.Delete,
            rule,
            true
        );

        return person;
    }

    public PersonJuridical? Save(PersonRules.PersistPersonJuridicalRule rule)
    {
        PersonJuridical? person = rule.Input.Id > 0 ? this.ExecuteQuery(
            this.personRepository.FindPersonJuridicalById,
            rule.Input.Id,
            false
        ) : null;

        // [TODO]: adicionar stack de erro
        if (
            person is not null &&
            (
                person.Version != rule.Input.Version ||
                person.EnterpriseId != rule.EnterpriseId ||
                person.PersonType != PersonType.Juridical
            )
        ) throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_PERSON_REQUIRED_DOCUMENT_NOT_INFORMED);

        return this.ExecuteQuery(
            this.personRepository.Save,
            new PersonRules.PersistPersonJuridicalDtoRule
            {
                EnterpriseId = rule.EnterpriseId,
                UserId = rule.UserId,
                Input = rule.ToDto(person),
            },
            true
        );
    }

    public PersonJuridical? Remove(PersonRules.RemovePersonJuridicalRule rule)
    {
        PersonJuridical? person = rule.Input.Id > 0 ? this.ExecuteQuery(
            this.personRepository.FindPersonJuridicalById,
            rule.Input.Id,
            false
        ) : null;

        // [TODO]: adicionar stack de erro
        if (
            person is null ||
            (
                person is not null &&
                (
                    person.Version != rule.Input.Version ||
                    person.EnterpriseId != rule.EnterpriseId ||
                    person.PersonType != PersonType.Juridical
                )
            )
        ) throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_PERSON_REQUIRED_DOCUMENT_NOT_INFORMED);

        this.ExecuteQuery(
            this.personRepository.Delete,
            rule,
            true
        );

        return person;
    }
}
