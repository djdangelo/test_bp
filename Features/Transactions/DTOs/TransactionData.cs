using test_bp.Shared.Settings;

namespace test_bp.Features.Transactions.DTOs
{
    public record CreateTransaction(
        int IdAccount,
        double Amount,
        TransactionType Type,
        int CreatedBy
     );
}
