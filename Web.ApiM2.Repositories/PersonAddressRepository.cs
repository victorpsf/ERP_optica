using Application.Database;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories
{
    public partial class PersonAddressRepository: PersonAddressSql
    {
        private readonly DbMysqlClientFactory Factory;

        public PersonAddressRepository(DbMysqlClientFactory factory) { this.Factory = factory; }
    }
}
