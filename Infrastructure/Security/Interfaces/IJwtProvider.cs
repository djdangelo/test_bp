using System.Security.Claims;

namespace test_bp.Infrastructure.Security.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateUserToken(int userId, int agencyId, string role);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
