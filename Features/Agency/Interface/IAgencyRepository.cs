using test_bp.Features.Agency.DTOs;

namespace test_bp.Features.Agency.Interface
{
    public interface IAgencyRepository
    {
        Task CreateAgency(Shared.Domain.Agency createAgency);
        Task SaveChangesAsync();
    }
}
