using test_bp.Features.SystemSetting.DTOs;
using test_bp.Features.SystemSetting.Helper;
using test_bp.Features.SystemSetting.Interface;
using test_bp.Shared.Messaging;

namespace test_bp.Features.SystemSetting.Service
{
    public class SystemSettingService (ISystemSettingRepository systemSettingRepository)
    {
        public async Task<ApiResponse<SettingResponse>> GetValueAsync(string key)
        {
            var setting = await systemSettingRepository.GetByKeyAsync(key);
            if (setting == null) return ApiResponse<SettingResponse>.NotFound($"La configuración '{key}' no existe.");

            return ApiResponse<SettingResponse>.Ok(SystemSettingHelper.ToResponse(setting));
        }

        public async Task<ApiResponse<SettingResponse>> CreateSetting(CreateSettingRequest createSettingRequest)
        {
            var data = SystemSettingHelper.toSystemSetting(createSettingRequest);
            await systemSettingRepository.CreateSetting(data);
            await systemSettingRepository.SaveChangesAsync();
            return ApiResponse<SettingResponse>.Created(SystemSettingHelper.ToResponse(data), "Configuración creada exitosamente.");
        }

        public async Task<ApiResponse<SettingResponse>> UpdateAsync(string key, UpdateSettingRequest request)
        {
            var setting = await systemSettingRepository.GetByKeyAsync(key);
            if (setting == null) return ApiResponse<SettingResponse>.NotFound("Configuración no encontrada.");

            setting.UpdateValue(request.Value, request.isActive);
            await systemSettingRepository.SaveChangesAsync();

            return ApiResponse<SettingResponse>.Ok(SystemSettingHelper.ToResponse(setting), "Configuración actualizada.");
        }

        public async Task<ApiResponse<IEnumerable<SettingResponse>>> GetAllAsync()
        {
            var settings = await systemSettingRepository.GetAllAsync();
            return ApiResponse<IEnumerable<SettingResponse>>.Ok(settings.Select(s => SystemSettingHelper.ToResponse(s)));
        }
    }
}
