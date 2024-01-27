﻿using Application.Base.Models;
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

    public PersonPhysical? Save(PersonRules.PersistPersonPhysicalRule rule)
    {
        PersonPhysical? person = rule.Input.Id > 0 ? this.ExecuteQuery(
            this.personRepository.FindById,
            rule.Input.Id,
            false
        ) : null;

        // [TODO]: adicionar stack de erro
        if (
            person is not null && 
            (
                person.Version != rule.Input.Version || 
                person.EnterpriseId != rule.EnterpriseId || 
                person.PersonType != PersonType.Physical
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
}
