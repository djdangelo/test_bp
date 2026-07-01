using test_bp.Shared.Settings;

namespace test_bp.Features.Transactions.Helper
{
    public static class TransactionHelper
    {
        public static double CalculateNewBalance(double currentBalance, double transactionAmount, TransactionType transactionType)
        {
            return transactionType switch
            {
                TransactionType.Deposit => currentBalance + transactionAmount,
                TransactionType.Withdrawal => currentBalance - transactionAmount,
                _ => throw new ArgumentException("Invalid transaction type", nameof(transactionType)),
            };
        }

        public static Shared.Domain.Transactions toTransaction(int idAccount, double amount, TransactionType type, int createdBy)
        {
            return new Shared.Domain.Transactions(idAccount, decimal.Parse(amount.ToString()), type, createdBy);
        }

    }
}
