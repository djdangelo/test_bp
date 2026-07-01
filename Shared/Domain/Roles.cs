namespace test_bp.Shared.Domain
{
    public class Roles
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }

        private Roles() { }

        public Roles(string name)
        {
            Name = name;
            IsActive = true;
        }

        public void Update(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
