using test_bp.Features.User.DTOs;

namespace test_bp.Features.User.Helper
{
    public static class UserHelper
    {
        public static UserResponse ToResponse(test_bp.Shared.Domain.Users user)
        {
            return new UserResponse(
                user.Id,
                user.UserName,
                user.IsActive,
                user.CreatedAt
            );
        }
    }
}
