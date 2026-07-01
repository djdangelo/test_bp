namespace test_bp.Features.Accounts.Interface
{
    public interface IAccountRepository
    {
        Task CreateAccount(Shared.Domain.Accounts account);
        Task<Shared.Domain.Accounts?> GetAccountByIdAsync(int idAccount);
        void UpdateAccount(Shared.Domain.Accounts account);
        Task SaveAsync();
    }
}
