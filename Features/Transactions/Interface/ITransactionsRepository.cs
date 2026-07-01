namespace test_bp.Features.Transactions.Interface
{
    public interface ITransactionsRepository
    {
        Task CreateTransaction(Shared.Domain.Transactions transaction);
        Task<int?> GetNumWithdrawalsAsync(int idAccount, DateTime startDate);
        Task SaveAsync();
    }
}
