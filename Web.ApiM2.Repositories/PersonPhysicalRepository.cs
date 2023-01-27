using Application.Database;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories
{
    public partial class PersonPhysicalRepository: PersonPhysicalSql
    {
        private readonly DbMysqlClientFactory Factory;

        public PersonPhysicalRepository(DbMysqlClientFactory factory)
        { this.Factory = factory; }
    }
}
