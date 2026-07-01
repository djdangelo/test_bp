using System.Security.Claims;
using test_bp.Features.User.DTOs;
using test_bp.Features.User.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.User
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var publicGroup = app.MapGroup("/api/users-public")
                .WithTags("Users")
                .AllowAnonymous();

            var group = app.MapGroup("/api/users")
                           .WithTags("Users")
                           .RequireAuthorization("AdminPolicy");

            publicGroup.MapPost("/", async (
                CreateUserRequest request, 
                UserService service, 
                ClaimsPrincipal user) =>
                (await service.CreateAsync(request, user)).ToResult());

            group.MapPut("/{id}", async (int id, UpdateUserRequest request, UserService service) =>
                (await service.UpdateUser(id, request)).ToResult());

            group.MapGet("/{id}", async (int id, UserService service) =>
                (await service.GetByIdAsync(id)).ToResult());

            group.MapGet("/", async (UserService service) => (await service.GetAllUser()).ToResult());
        }
    }
}
