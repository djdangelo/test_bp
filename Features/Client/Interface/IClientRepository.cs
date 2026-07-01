namespace test_bp.Features.Client.Interface
{
    public interface IClientRepository
    {
        Task<Shared.Domain.Clients?> GetByIdAsync(int id);
        Task<IEnumerable<Shared.Domain.Clients>> GetAllGeneral();
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(Shared.Domain.Clients client);
        Task SaveChangesAsync();
    }
}
