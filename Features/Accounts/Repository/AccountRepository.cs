using Microsoft.EntityFrameworkCore;
using test_bp.Features.Accounts.Interface;
using test_bp.Infrastructure.Persistense;

namespace test_bp.Features.Accounts.Repository
{
    public class AccountRepository(BpContext context) : IAccountRepository
    {
        public async Task CreateAccount(Shared.Domain.Accounts account) => await context.Accounts.AddAsync(account);

        public async Task<Shared.Domain.Accounts?> GetAccountByIdAsync(int idAccount) => await context.Accounts.FirstOrDefaultAsync(a => a.Id == idAccount);

        public void UpdateAccount(Shared.Domain.Accounts account) => context.Accounts.Update(account);

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
