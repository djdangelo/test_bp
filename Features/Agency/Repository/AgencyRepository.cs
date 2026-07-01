using test_bp.Features.Agency.Interface;
using test_bp.Infrastructure.Persistense;

namespace test_bp.Features.Agency.Repository
{
    public class AgencyRepository(BpContext context) : IAgencyRepository
    {
        public async Task CreateAgency(Shared.Domain.Agency createAgency) => await context.Agency.AddAsync(createAgency);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();
    }
}
