namespace test_bp.Shared.Domain
{
    public class Agency
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public Agency() { }

        public Agency(string name)
        {
            Name = name;
            IsActive = true;
        }
    }
}
