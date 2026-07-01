using test_bp.Shared.Settings;

namespace test_bp.Shared.Domain
{
    public class Transactions
    {
        public Guid Id { get; private set; }
        public int IdAccount { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int CreatedBy { get; private set; }

        public Users CreatedByUser { get; private set; } = null!;

        public Transactions() { }

        public Transactions(int idAccount, decimal amount, TransactionType type, int createdBy)
        {
            Id = Guid.NewGuid();
            IdAccount = idAccount;
            Amount = amount;
            Type = type;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
