namespace test_bp.Features.Client.DTOs
{
    public record UpdateClientRequest(
        string Name,
        string LastName,
        string Email,
        string Phone,
        string Dni,
        bool IsActive
    );
}
