using test_bp.Features.SystemSetting.DTOs;

namespace test_bp.Features.SystemSetting.Helper
{
    public static class SystemSettingHelper
    {
        public static SettingResponse ToResponse(this Shared.Domain.SystemSetting setting) =>
        new(setting.Key, setting.Value, setting.Description, setting.UpdatedAt);

        public static Shared.Domain.SystemSetting toSystemSetting(CreateSettingRequest createSettingRequest)
            => new Shared.Domain.SystemSetting(createSettingRequest.key.Trim().ToUpper(), createSettingRequest.value, createSettingRequest.description);
    }
}
