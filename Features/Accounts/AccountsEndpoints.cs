using System.Security.Claims;
using test_bp.Features.Accounts.DTOs;
using test_bp.Features.Accounts.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Accounts
{
    public static class AccountsEndpoints
    {
        public static void MapAccountsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/accounts")
                .WithTags("Accounts")
                .RequireAuthorization("ClientAdminPolicy");

            group.MapPost("/", async (AccountsData request, AccountService accountService, ClaimsPrincipal claimsUser) =>
                (await accountService.CreateAccount(request, claimsUser)).ToResult()
            );

        }
    }
}
