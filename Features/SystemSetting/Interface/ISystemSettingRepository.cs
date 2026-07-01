using test_bp.Features.SystemSetting.DTOs;

namespace test_bp.Features.SystemSetting.Interface
{
    public interface ISystemSettingRepository
    {
        Task<Shared.Domain.SystemSetting?> GetByKeyAsync(string key);
        Task CreateSetting(Shared.Domain.SystemSetting systemSetting);
        Task<IEnumerable<Shared.Domain.SystemSetting>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
