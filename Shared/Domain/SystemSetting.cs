namespace test_bp.Shared.Domain
{
    public class SystemSetting
    {
        public int Id { get; private set; }
        public string Key { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public SystemSetting() { }

        public SystemSetting(string key, string value, string? description)
        {
            Key = key.Trim().ToUpper();
            Value = value;
            Description = description;
            UpdatedAt = DateTime.Now;
            IsActive = true;
        }
        public void UpdateValue(string newValue, bool isActive)
        {
            Value = newValue;
            UpdatedAt = DateTime.UtcNow;
            IsActive = isActive;
        }
    }
}
