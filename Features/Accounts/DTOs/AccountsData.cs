namespace test_bp.Features.Accounts.DTOs
{
    public record AccountsData(
        int IdClient,
        double CurrentBalance,
        double LastBalance,
        bool IsActive,
        int CreatedBy
    );
}
