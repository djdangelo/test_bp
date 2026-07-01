namespace test_bp.Shared.Domain
{
    public class Users
    {
        public int Id { get; private set; }
        public int AgencyId { get; private set; }
        public int IdRole { get; private set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Agency Agency { get; private set; } = null!;
        public Roles Roles { get; private set; } = null!;

        public Users() { }

        public Users(int agencyId, int idRole, string userName, string passwordHash, bool isActive  )
        {
            AgencyId = agencyId;
            IdRole = idRole;
            UserName = userName;
            PasswordHash = passwordHash;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            IsActive = isActive;
        }

        public void setStatus(bool isActive)
        {
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
