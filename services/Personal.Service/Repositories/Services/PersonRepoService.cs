using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces.Connections;
using Application.Services;
using Application.Utils;
using Personal.Service.Repositories.Rules;
using System.Data;
using static Application.Base.Models.BasePatternsModels;
using static Application.Dtos.PersonDtos;
using static Personal.Service.Controllers.Models.PersonModels;

namespace Personal.Service.Repositories.Services;


public partial class PersonRepoService : BaseRepoService<IPersonalDatabase>
{
    private PersonRepository personRepository;
    private DocumentRepository documentRepository;

    public PersonRepoService(IPersonalDatabase db) : base(db)
    {
        this.personRepository = new PersonRepository(db);
        this.documentRepository = new DocumentRepository(db);
    }

    public async Task<PaginationOutput<PersonPhysicalInput, PersonPhysical>> Get(PersonRules.FindPersonPhysicalWithPaginationRule rule)
    {
        var pagination = this.ExecuteQuery(
            this.personRepository.Pagination,
            rule,
            false
        );

        var persons = this.ExecuteQuery(
            this.personRepository.ListPerPagination,
            rule,
            false
        );

        if (persons is not null)
        {
            List<DocumentDtos.Document> documents = new List<DocumentDtos.Document>();
            await this.ExecuteQueryAsync(
                () => this.documentRepository.ListByPersonIds(persons.Select(a => a.Id).ToList()),
                (List<DocumentDtos.Document>? _documents) => 
                {
                    if (_documents is null) return;
                    documents = _documents;
                }
            );
            foreach (var person in persons)
                person.Documents = documents.Where(a => a.PersonId == person.Id).ToList();
        }

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

    public Task<PaginationOutput<PersonJuridicalInput, PersonJuridical>> Get(PersonRules.FindPersonJuridicalWithPaginationRule rule)
    {
        var pagination = this.ExecuteQuery(
            this.personRepository.Pagination,
            rule,
            false
        );

        var persons = this.ExecuteQuery(
            this.personRepository.ListPerPagination,
            rule,
            false
        );

        return Task.FromResult(new PaginationOutput<PersonJuridicalInput, PersonJuridical>
        {
            Total = pagination?.Total ?? 0,
            Page = pagination?.Page ?? 0,
            PerPage = pagination?.PerPage ?? 0,
            TotalPages = pagination?.TotalPages ?? 0,
            Values = persons ?? new List<PersonJuridical>(),
            Search = rule.Search
        });
    }

    public async Task<PersonPhysical?> Save(PersonRules.PersistPersonPhysicalRule rule)
    {
        PersonPhysical? person = rule.Input.Id > 0 ? this.ExecuteQuery(
            this.personRepository.FindPersonPhysicalById,
            rule.Input.Id,
            false
        ) : null;

        // [TODO]: adicionar stack de erro
        if (
            (rule.Input.Id > 0 && person is null) ||
            (
                person is not null &&
                (
                    person.Version != rule.Input.Version ||
                    person.EnterpriseId != rule.EnterpriseId ||
                    person.PersonType != PersonType.Physical
                )
            )
        ) throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_PERSON_REQUIRED_DOCUMENT_NOT_INFORMED);

        if (person is not null)
            await TaskManager.GetInstance()
                .Add(
                    this.ExecuteQueryAsync<List<DocumentDtos.Document>>(
                        () => this.documentRepository.ListByPersonId(person.Id),
                        (List<DocumentDtos.Document>? values) =>
                        {
                            if (values is null) return;
                            person.Documents = values;
                        }
                    )
                ).AwaitAll();

        PersonPhysical? createdPerson = null;
        List<DocumentDtos.Document> documents = new List<DocumentDtos.Document>();

        await this.ExecuteAnyAsync(
            true,
            new ExecuteAnyArgument
            {
                Caller = () => this.personRepository.Save(new PersonRules.PersistPersonPhysicalDtoRule
                {
                    EnterpriseId = rule.EnterpriseId,
                    UserId = rule.UserId,
                    Input = rule.ToDto(person),
                }),
                ReturnType = (BaseDto? value) =>
                {
                    if (value is null) return;
                    createdPerson = value as PersonPhysical;
                }
            }
        );

        if (createdPerson is not null)
        {
            createdPerson.Documents = documents;
        }

        return createdPerson;
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
