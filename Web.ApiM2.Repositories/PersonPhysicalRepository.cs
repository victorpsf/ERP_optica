using Application.Database.Interfaces;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories
{
    public partial class PersonPhysicalRepository: PersonPhysicalSql
    {
        private readonly IM2Database Factory;

        public PersonPhysicalRepository(IM2Database factory)
        { this.Factory = factory; }
    }
}
