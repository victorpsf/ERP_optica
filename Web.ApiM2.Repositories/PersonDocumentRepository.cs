using Application.Database;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories
{
    public partial class PersonDocumentRepository: PersonDocumentSql
    {
        private readonly DbMysqlClientFactory Factory;

        public PersonDocumentRepository(DbMysqlClientFactory factory) { this.Factory = factory; }
    }
}
