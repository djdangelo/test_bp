using test_bp.Features.User.DTOs;

namespace test_bp.Features.User.Interface
{
    public interface IUserRepository
    {
        Task<test_bp.Shared.Domain.Users?> GetByIdAsync(int id);
        Task<test_bp.Shared.Domain.Users?> GetByUserNameAsync(string username);
        Task<List<UserResponse>> GetAll();  
        void UpdateUser(test_bp.Shared.Domain.Users user);
        Task<bool> ExistsByUsernameAsync(string username);
        Task AddAsync(test_bp.Shared.Domain.Users user);
        Task SaveChangesAsync();
    }
}
