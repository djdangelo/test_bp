using FluentValidation;
using test_bp.Features.Client.DTOs;
using System.Security.Claims;
using test_bp.Features.Client.Interface;
using test_bp.Features.User.Interface;
using test_bp.Shared.Messaging;
using test_bp.Infrastructure.Security.Extensions;
using test_bp.Features.Client.Helper;

namespace test_bp.Features.Client.Service
{
    public class ClientService(
        IClientRepository repository,
        IUserRepository userRepository,
        IValidator<CreateClientRequest> createValidator,
        IValidator<UpdateClientRequest> updateValidator,
        ILogger<ClientService> logger)
    {
        

        public async Task<ApiResponse<ClientResponse>> CreateAsync(
            CreateClientRequest request, 
            ClaimsPrincipal claimsPrincipal)
        {
            int userId = claimsPrincipal.GetUserId();
            int companyId = claimsPrincipal.GetAgencyId();

            logger.LogInformation("Intento de creación de cliente por usuario ID {UserId} en compañía ID {CompanyId}", userId, companyId);

            var validation = await createValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                logger.LogWarning("Creación de cliente fallido por validación: {Error}", validation.Errors.FirstOrDefault()?.ErrorMessage);
                return ApiResponse<ClientResponse>.BadRequest(validation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            if (userId == 0 || companyId == 0)
            {
                logger.LogWarning("Creación de cliente fallido: token inválido o claims vacíos (UserId={UserId}, CompanyId={CompanyId})", userId, companyId);
                return ApiResponse<ClientResponse>.Unauthorized("Token inválido, corrupto o sin permisos.");
            }

            if (!string.IsNullOrEmpty(request.Email) && await repository.ExistsByEmailAsync(request.Email))
            {
                logger.LogWarning("Creación de cliente fallido: ya existe un cliente con el correo {Email} en la compañía ID {CompanyId}", request.Email, companyId);
                return ApiResponse<ClientResponse>.BadRequest("Ya existe un cliente con este correo en la compañía.");
            }

            var client = new Shared.Domain.Clients(
                request.Name,
                request.LastName,
                request.Email,
                request.Phone,
                request.Dni,
                userId
            );

            await repository.AddAsync(client);
            await repository.SaveChangesAsync();

            logger.LogInformation("Cliente creado exitosamente con ID {ClientId} para la compañía ID {CompanyId}", client.Id, companyId);

            return ApiResponse<ClientResponse>.Created(ClientHelper.ToResponse(client), "Dato registrado correctamente.");
        }

        public async Task<ApiResponse<ClientResponse>> UpdateAsync(int id, UpdateClientRequest request, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.GetUserId();

            logger.LogInformation("Intento de actualización de cliente ID {ClientId}", id);

            var validation = await updateValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                logger.LogWarning("Actualización de cliente ID {ClientId} fallido por validación: {Error}", id, validation.Errors.FirstOrDefault()?.ErrorMessage);
                return ApiResponse<ClientResponse>.BadRequest(validation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var client = await repository.GetByIdAsync(id);
            if (client == null)
            {
                logger.LogWarning("Actualización de cliente fallida: cliente ID {ClientId} no encontrado", id);
                return ApiResponse<ClientResponse>.NotFound("Cliente no encontrado.");
            }

            if (client.Email != request.Email && !string.IsNullOrEmpty(request.Email))
            {
                if (await repository.ExistsByEmailAsync(request.Email))
                {
                    logger.LogWarning("Actualización de cliente fallida: el correo {Email} ya está en uso en la compañía ID {CompanyId}", request.Email);
                    return ApiResponse<ClientResponse>.BadRequest("El correo electrónico ya está en uso por otro cliente.");
                }
            }

            client.Update(request.Name, request.LastName, request.Email, request.Phone, request.Dni, request.IsActive, userId );
            await repository.SaveChangesAsync();

            logger.LogInformation("Cliente ID {ClientId} actualizado exitosamente", id);

            return ApiResponse<ClientResponse>.Ok(client.ToResponse());
        }

        public async Task<ApiResponse<IEnumerable<ClientResponse>>> GetAllGeneral()
        {
            logger.LogInformation("Obteniendo listado general de todos los clientes (Modo Admin General)");
            var clients = await repository.GetAllGeneral();
            logger.LogInformation("Listado general de clientes obtenido exitosamente. Cantidad: {Count}", clients.Count());
            return ApiResponse<IEnumerable<ClientResponse>>.Ok(clients.Select(c => c.ToResponse()));
        }

        public async Task<ApiResponse<ClientResponse>> GetByIdAsync(int id)
        {
            logger.LogInformation("Obteniendo cliente por ID {ClientId}", id);
            var client = await repository.GetByIdAsync(id);
            if (client == null)
            {
                logger.LogWarning("Cliente ID {ClientId} no encontrado", id);
                return ApiResponse<ClientResponse>.NotFound("Cliente no encontrado.");
            }

            logger.LogInformation("Cliente ID {ClientId} obtenido con éxito", id);
            return ApiResponse<ClientResponse>.Ok(client.ToResponse());
        }
    }
}
