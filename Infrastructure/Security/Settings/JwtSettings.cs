namespace test_bp.Infrastructure.Security.Settings
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string KeyToken { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }
}
