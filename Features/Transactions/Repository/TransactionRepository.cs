using test_bp.Features.Transactions.Interface;
using test_bp.Infrastructure.Persistense;

namespace test_bp.Features.Transactions.Repository
{
    public class TransactionRepository(
        BpContext context
    ) : ITransactionsRepository
    {
        public async Task CreateTransaction(Shared.Domain.Transactions transaction) 
            => await context.Transaction.AddAsync(transaction);

        public async Task<int?> GetNumWithdrawalsAsync(int idAccount, DateTime startDate)
        {
            var data = context.Transaction
                .Where(t => t.IdAccount == idAccount && t.Type == Shared.Settings.TransactionType.Withdrawal && t.CreatedAt >= startDate)
                .Count();
            return data;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
