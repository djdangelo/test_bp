namespace test_bp.Features.Role.DTOs
{
    public record CreateRoleRequest(
        string Name
    );

    public record UpdateRoleRequest(
        string Name,
        bool IsActive
    );

    public record RoleResponse(
        int Id,
        string Name,
        bool IsActive
    );
}
