using Application.Database;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories
{
    public partial class PersonContactRepository: PersonContactSql
    {
        private readonly DbMysqlClientFactory Factory;

        public PersonContactRepository(DbMysqlClientFactory factory) { this.Factory = factory; }
    }
}
