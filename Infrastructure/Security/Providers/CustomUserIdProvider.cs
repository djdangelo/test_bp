using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace test_bp.Infrastructure.Security.Providers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                   ?? connection.User?.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
        }
    }
}
