using test_bp.Features.Accounts.DTOs;

namespace test_bp.Features.Accounts.Helper
{
    public static class AccountHelper
    {
        public static AccountsData ToAccountsData(this Shared.Domain.Accounts account)
        {
            return new AccountsData(
                account.IdClient,
                account.CurrentBalance,
                account.LastBalance,
                account.IsActive,
                account.CreatedBy
            );
        }

        public static Shared.Domain.Accounts ToDomain(this AccountsData accountData)
        {
            return new Shared.Domain.Accounts(
                accountData.IdClient,
                accountData.CurrentBalance,
                accountData.LastBalance,
                accountData.IsActive,
                accountData.CreatedBy
            );
        }

        public static bool ValidateMinimunBalance(double minimunBalance, double currentBalance)
        {
            return currentBalance >= minimunBalance;
        }
    }
}
