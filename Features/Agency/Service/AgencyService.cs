using test_bp.Features.Agency.DTOs;
using test_bp.Features.Agency.Helper;
using test_bp.Features.Agency.Interface;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Agency.Service
{
    public class AgencyService(IAgencyRepository agencyRepository)
    {
        public async Task<ApiResponse<AgencyResponse>> CreateAgency(CreateAgency createAgencyRequest)
        {
            var data = AgencyHelper.ToAgency(createAgencyRequest);
            await agencyRepository.CreateAgency(data);
            await agencyRepository.SaveChangesAsync();
            return ApiResponse<AgencyResponse>.Created(AgencyHelper.ToResponse(data), "Agencia creada exitosamente.");
        }
    }
}
