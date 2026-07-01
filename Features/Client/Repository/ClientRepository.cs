using Microsoft.EntityFrameworkCore;
using test_bp.Features.Client.Interface;
using test_bp.Infrastructure.Persistense;
namespace Nexo.Api.Features.Client.Repository
{
    public class ClientRepository(BpContext context) : IClientRepository
    {
        public async Task<test_bp.Shared.Domain.Clients?> GetByIdAsync(int id) =>
        await context.Clients
            .Include(c => c.CreatedBy)
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

        public async Task<bool> ExistsByEmailAsync(string email) =>
            !string.IsNullOrEmpty(email) &&
            await context.Clients.AnyAsync(c => c.Email == email);

        public async Task AddAsync( test_bp.Shared.Domain.Clients client) => await context.Clients.AddAsync(client);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task<IEnumerable<test_bp.Shared.Domain.Clients>> GetAllGeneral() => await context.Clients
                .Include(c => c.CreatedByUser)
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

    }
}
