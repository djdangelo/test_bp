using test_bp.Features.Auth.DTOs;
using test_bp.Features.Auth.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Auth
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth").WithTags("Auth");

            group.MapPost("/login", async (LoginRequest request, AuthService service) =>
                (await service.LoginAsync(request)).ToResult())
                .AllowAnonymous();

        }
    }
}
