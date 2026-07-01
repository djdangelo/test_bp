namespace test_bp.Features.SystemSetting.DTOs
{
    public record UpdateSettingRequest(string Value, bool isActive);
    public record CreateSettingRequest(string key, string value, string? description);
    public record SettingResponse(
        string Key,
        string Value,
        string? Description,
        DateTime UpdatedAt
    );
}
