namespace test_bp.Features.Auth.DTOs
{
    public record UserProfile(
        int Id,
        int AgencyId,
        string UserName,
        string Role
     );
}
