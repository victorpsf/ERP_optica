using Application.Database.Interfaces;
using Web.ApiM2.Repositories.Queries;

namespace Web.ApiM2.Repositories;

public partial class PersonAddressRepository: PersonAddressSql
{
    private readonly IM2Database Factory;

    public PersonAddressRepository(IM2Database factory) { this.Factory = factory; }
}
