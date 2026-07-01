using Microsoft.EntityFrameworkCore;
using test_bp.Features.SystemSetting.Interface;
using test_bp.Infrastructure.Persistense;

namespace test_bp.Features.SystemSetting.Repository
{
    public class SystemSettingRepository(BpContext context) : ISystemSettingRepository
    {
        public async Task<Shared.Domain.SystemSetting?> GetByKeyAsync(string key) =>
        await context.SystemSettings.FirstOrDefaultAsync(s => s.Key == key.ToUpper());

        public async Task<IEnumerable<Shared.Domain.SystemSetting>> GetAllAsync() =>
            await context.SystemSettings.ToListAsync();

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task CreateSetting(Shared.Domain.SystemSetting system) 
            => await context.SystemSettings.AddAsync(system);
    }
}
