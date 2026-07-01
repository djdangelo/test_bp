using Microsoft.EntityFrameworkCore;
using test_bp.Features.User.DTOs;
using test_bp.Features.User.Interface;
using test_bp.Infrastructure.Persistense;

namespace test_bp.Features.User.Repository
{
    public class UserRepository(BpContext context) : IUserRepository
    {
        public async Task<Shared.Domain.Users?> GetByIdAsync(int id) =>
        await context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<Shared.Domain.Users?> GetByUserNameAsync(string username) =>
            await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName == username);

        public async Task<bool> ExistsByUsernameAsync(string username) =>
            await context.Users.AnyAsync(u => u.UserName == username);

        public async Task AddAsync(Shared.Domain.Users user) => await context.Users.AddAsync(user);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task<List<UserResponse>> GetAll() => 
            await context.Users.Where(a => a.IsActive)
            .Select(a => new UserResponse(
            a.Id,
            a.UserName,
            a.IsActive,
            a.CreatedAt
        )).ToListAsync();

        public void UpdateUser(Shared.Domain.Users user) => context.Users.Update(user);
    }
}
