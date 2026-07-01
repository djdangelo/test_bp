using test_bp.Features.Role.DTOs;

namespace test_bp.Features.Role.Helper
{
    public static class RoleHelper
    {
        public static RoleResponse ToResponse(test_bp.Shared.Domain.Roles role)
        {
            return new RoleResponse(
                role.Id,
                role.Name,
                role.IsActive
            );
        }
    }
}
