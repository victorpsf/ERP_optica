using Application.Database;
using Web.ApiM1.Repositories.Queries;

namespace Web.ApiM1.Repositories
{
    public partial class EnterpriseRepository : EnterpriseSql
    {
        private readonly DbMysqlClientFactory Factory;

        public EnterpriseRepository(DbMysqlClientFactory factory) { this.Factory = factory; }
    }
}
