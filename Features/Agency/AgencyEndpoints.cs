using test_bp.Features.Agency.DTOs;
using test_bp.Features.Agency.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Agency
{
    public static class AgencyEndpoints
    {
        public static void MapAgencyEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/agency")
                           .WithTags("Agencies")
                           .AllowAnonymous(); //.RequireAuthorization("AdminPolicy");
            group.MapPost("/", async (CreateAgency request, AgencyService service) =>
                (await service.CreateAgency(request)).ToResult());

        }
    }
}
