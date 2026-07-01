namespace test_bp.Features.Auth.DTOs
{
    public record AuthResponse(
        string Token,
        UserProfile Profile
    );
}
