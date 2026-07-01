namespace test_bp.Features.User.DTOs
{
    public record UpdateUserRequest(
        string UserName,
        bool IsActive,
        int RoleId 
     );
}
