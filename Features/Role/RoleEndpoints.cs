using test_bp.Features.Role.DTOs;
using test_bp.Features.Role.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Role
{
    public static class RoleEndpoints
    {
        public static void MapRoleEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/roles")
                           .WithTags("Roles")
                           .RequireAuthorization("AdminPolicy");

            group.MapPost("/", async (CreateRoleRequest request, RoleService service) =>
                (await service.CreateAsync(request)).ToResult());

            group.MapPut("/{id}", async (int id, UpdateRoleRequest request, RoleService service) =>
                (await service.UpdateAsync(id, request)).ToResult());

            group.MapGet("/", async (RoleService service) => (await service.GetAllAsync()).ToResult());
        }
    }
}
