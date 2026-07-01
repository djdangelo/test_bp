using test_bp.Features.Role.DTOs;

namespace test_bp.Features.Role.Interface
{
    public interface IRoleRepository
    {
        Task<Shared.Domain.Roles?> GetByIdAsync(int id);
        Task<IEnumerable<Shared.Domain.Roles>> GetAllAsync();
        Task<bool> ExistsByNameAsync(string name);
        Task AddAsync(Shared.Domain.Roles role);
        Task SaveChangesAsync();
    }
}
