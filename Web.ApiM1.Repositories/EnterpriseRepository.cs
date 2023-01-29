using Application.Database.Interfaces;
using Web.ApiM1.Repositories.Queries;

namespace Web.ApiM1.Repositories
{
    public partial class EnterpriseRepository : EnterpriseSql
    {
        private readonly IM1Database Factory;

        public EnterpriseRepository(IM1Database factory) { this.Factory = factory; }
    }
}
