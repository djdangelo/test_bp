namespace test_bp.Features.Agency.DTOs
{
    public record CreateAgency(string Name);
    public record AgencyResponse(int Id, string Name, bool IsActive);
}
