using Application.Database.Interfaces;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories;

public partial class PersonDocumentRepository: PersonDocumentSql
{
    private readonly IM2Database Factory;

    public PersonDocumentRepository(IM2Database factory) { this.Factory = factory; }
}
