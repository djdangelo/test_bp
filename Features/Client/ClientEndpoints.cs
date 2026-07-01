using System.Security.Claims;
using test_bp.Features.Client.DTOs;
using test_bp.Features.Client.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Client
{
    public static class ClientEndpoints
    {
        public static void MapClientEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/clients")
                           .WithTags("Clients");
            
            var protectedGroup = group.RequireAuthorization("ClientAdminPolicy");

            protectedGroup.MapPost("/", async (CreateClientRequest request, ClientService service, ClaimsPrincipal claimsUser) => 
                (await service.CreateAsync(request, claimsUser)).ToResult()
            );

            protectedGroup.MapPut("/{id}", async (int id, UpdateClientRequest request, ClientService service, ClaimsPrincipal user) =>
                (await service.UpdateAsync(id, request, user)).ToResult());

            protectedGroup.MapGet("/{id}", async (int id, ClientService service) =>
                (await service.GetByIdAsync(id)).ToResult());

            protectedGroup.MapGet("/all", async (ClientService service) =>
                (await service.GetAllGeneral()).ToResult());

        }
    }
}
