namespace test_bp.Features.Auth.DTOs
{
    public record LoginRequest(
        string UserName,
        string Password
    );
}
