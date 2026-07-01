namespace test_bp.Shared.Domain
{
    public class Accounts
    {
        public int Id { get; private set; }
        public int IdClient { get; private set; }
        public double CurrentBalance { get; private set; }
        public double LastBalance { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }
        public int CreatedBy { get; private set; }
        public int? UpdatedBy { get; private set; }

        public Clients Client { get; private set; } = null!;
        public Users CreatedByUser { get; private set; } = null!;
        public Users UpdatedByUser { get; private set; } = null!;

        public Accounts() { }

        public Accounts(int idClient, double currentBalance, double lastBalance, bool isActive, int createdBy)
        {
            IdClient = idClient;
            CurrentBalance = currentBalance;
            LastBalance = lastBalance;
            IsActive = isActive;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void Update(double currentBalance, double lastBalance, bool isActive, int updatedBy)
        {
            CurrentBalance = currentBalance;
            LastBalance = lastBalance;
            IsActive = isActive;
            UpdatedBy = updatedBy;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
