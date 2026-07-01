namespace test_bp.Features.Client.DTOs
{
    public record ClientResponse(
        int Id,
        string Name,
        string LastName,
        string Email,
        string Phone,
        string Dni,
        bool IsActive,
        DateTime CreatedAt,
        string CreatedByUserName
        );
}
