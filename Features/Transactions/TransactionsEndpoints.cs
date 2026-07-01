using System.Security.Claims;
using test_bp.Features.Transactions.DTOs;
using test_bp.Features.Transactions.Service;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Transactions
{
    public static class TransactionsEndpoints
    {
        public static void MapTransactionsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/transactions")
                .WithTags("Transactions")
                .RequireAuthorization("RegisterTransactionPolicy");

            group.MapPost("/", async (CreateTransaction request, TransactionService transactionsService, ClaimsPrincipal claimsUser) =>
                (await transactionsService.CreateTransaction(request, claimsUser)).ToResult()
            );
        }
    }
}
