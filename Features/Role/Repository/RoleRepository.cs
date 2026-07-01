using Microsoft.EntityFrameworkCore;
using test_bp.Features.Role.Interface;
using test_bp.Infrastructure.Persistense;

namespace test_bp.Features.Role.Repository
{
    public class RoleRepository(BpContext context) : IRoleRepository
    {
        public async Task<Shared.Domain.Roles?> GetByIdAsync(int id) =>
        await context.Roles
            .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IEnumerable<Shared.Domain.Roles>> GetAllAsync() =>
            await context.Roles
                .Where(a => a.IsActive)
                .ToListAsync();

        public async Task<bool> ExistsByNameAsync(string name) =>
            await context.Roles.AnyAsync(r => r.Name == name);

        public async Task AddAsync(Shared.Domain.Roles role) => await context.Roles.AddAsync(role);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();
    }
}
