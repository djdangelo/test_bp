using test_bp.Features.Role.DTOs;
using test_bp.Features.Role.Helper;
using test_bp.Features.Role.Interface;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Role.Service
{
    public class RoleService(IRoleRepository repository)
    {
        public async Task<ApiResponse<RoleResponse>> CreateAsync(CreateRoleRequest request)
        {
            if (await repository.ExistsByNameAsync(request.Name))
                return ApiResponse<RoleResponse>.BadRequest("Ya existe un rol con ese nombre.");

            var role = new Shared.Domain.Roles(request.Name);

            await repository.AddAsync(role);
            await repository.SaveChangesAsync();

            return ApiResponse<RoleResponse>.Created(RoleHelper.ToResponse(role), "Role creado correctamente.");
        }

        public async Task<ApiResponse<RoleResponse>> UpdateAsync(int id, UpdateRoleRequest request)
        {
            var role = await repository.GetByIdAsync(id);
            if (role == null) return ApiResponse<RoleResponse>.NotFound("Rol no encontrado.");

            if (role.Name != request.Name && await repository.ExistsByNameAsync(request.Name))
                return ApiResponse<RoleResponse>.BadRequest("El nombre del rol ya está en uso.");

            role.Update(request.Name, request.IsActive);

            await repository.SaveChangesAsync();

            return ApiResponse<RoleResponse>.Ok(RoleHelper.ToResponse(role), "Rol actualizado corretamente.");
        }

        public async Task<ApiResponse<IEnumerable<RoleResponse>>> GetAllAsync()
        {
            var roles = await repository.GetAllAsync();
            return ApiResponse<IEnumerable<RoleResponse>>.Ok(roles.Select(r => RoleHelper.ToResponse(r)));
        }
    }
}
