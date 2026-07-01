using test_bp.Features.SystemSetting.DTOs;
using test_bp.Features.SystemSetting.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.SystemSetting
{
    public static class SystemSettingEndpoints
    {
        public static void MapSettingEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/settings")
                           .WithTags("System Settings")
                           .RequireAuthorization("AdminPolicy");

            group.MapPost("/", async (CreateSettingRequest request, SystemSettingService service) =>
                (await service.CreateSetting(request)).ToResult());

            group.MapGet("/", async (SystemSettingService service) =>
                (await service.GetAllAsync()).ToResult());

            group.MapGet("/{key}", async (string key, SystemSettingService service) =>
                (await service.GetValueAsync(key)).ToResult());

            group.MapPut("/{key}", async (string key, UpdateSettingRequest request, SystemSettingService service) =>
                (await service.UpdateAsync(key, request)).ToResult());
        }
    }
}
