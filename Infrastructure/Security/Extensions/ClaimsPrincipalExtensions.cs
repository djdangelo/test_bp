using System.Security.Claims;

namespace test_bp.Infrastructure.Security.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirstValue("sub") ?? user.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(value, out var id) ? id : 0;
        }

        public static int GetAgencyId(this ClaimsPrincipal user)
        {
            var value = user.FindFirstValue("agencyId");
            return int.TryParse(value, out var id) ? id : 0;
        }
    }
}
