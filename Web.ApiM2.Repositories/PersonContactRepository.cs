using Application.Database.Interfaces;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories
{
    public partial class PersonContactRepository: PersonContactSql
    {
        private readonly IM2Database Factory;

        public PersonContactRepository(IM2Database factory) { this.Factory = factory; }
    }
}
