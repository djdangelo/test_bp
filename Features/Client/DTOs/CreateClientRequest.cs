namespace test_bp.Features.Client.DTOs
{
    public record CreateClientRequest(
        string Name,
        string LastName,
        string Email,
        string Phone, string Dni);
}
