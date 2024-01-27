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

    //public List<PersonPhysical> List(PersonRules.FindPersonPhysicalRule rule)
    //{
    //    this.Build(rule, out DatabaseModels.ParameterCollection Parameters, out string Sql);
    //    List<PersonPhysical> results = new List<PersonPhysical>();

    //    try
    //    { this.db.Connect(); }

    //    catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

    //    try
    //    {
    //        results.AddRange(
    //            this.personRepository.FindByQuery(Sql, Parameters)
    //        );
    //    }

    //    catch (Exception ex)
    //    {
    //        this.db.Rollback();
    //        this.db.Disconnect();
    //        throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex);
    //    }

    //    try
    //    { this.db.Disconnect(); }

    //    catch (Exception ex)
    //    { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

    //    return results;
    //} 

    public PersonPhysical? Create(PersonRules.PersistPersonPhysicalRule rule)
    {
        PersonPhysical? person = null;

        try
        { this.db.Connect(); }

        catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        {
            person = this.personRepository.Save(rule);
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
