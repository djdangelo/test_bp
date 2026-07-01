namespace test_bp.Features.User.DTOs
{
    public record UserResponse(
            int Id,
            string UserName,
            bool IsActive,
            DateTime CreatedAt
        );
}
