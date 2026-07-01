namespace test_bp.Features.User.DTOs
{
    public record CreateUserRequest(
            string UserName,
            string Password,
            int RoleId
        );
}
