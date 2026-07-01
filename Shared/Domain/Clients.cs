namespace test_bp.Shared.Domain
{
    public class Clients
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Dni { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public int CreatedBy { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Users CreatedByUser { get; private set; } = null!;
        public Users UpdateByUser { get; private set; } = null!;

        public Clients() { }
        public Clients(string name, string lastName, string email, string phone, string dni, int createdBy)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Dni = dni;
            IsActive = true;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string lastName, string email, string phone, string dni, bool isActive, int updatedBy)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Dni = dni;
            IsActive = isActive;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
