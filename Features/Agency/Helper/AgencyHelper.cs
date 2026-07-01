using test_bp.Features.Agency.DTOs;

namespace test_bp.Features.Agency.Helper
{
    public static class AgencyHelper
    {
        public static AgencyResponse ToResponse(this Shared.Domain.Agency agency) =>
            new(agency.Id, agency.Name, agency.IsActive);
        public static Shared.Domain.Agency ToAgency(CreateAgency createAgency) =>
            new(createAgency.Name);
    }
}
