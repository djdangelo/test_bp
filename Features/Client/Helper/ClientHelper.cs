using test_bp.Features.Client.DTOs;

namespace test_bp.Features.Client.Helper
{
    public static class ClientHelper
    {
        public static ClientResponse ToResponse(this Shared.Domain.Clients client)
        {
            return new ClientResponse(
                client.Id,
                client.Name,
                client.LastName,
                client.Email,
                client.Phone,
                client.Dni,
                client.IsActive,
                client.CreatedAt,
                client.CreatedByUser.UserName ?? "Desconocido"
            );
        }
    }
}
