using System.Security.Claims;
using test_bp.Features.User.DTOs;
using test_bp.Features.User.Helper;
using test_bp.Features.User.Interface;
using test_bp.Infrastructure.Security.Extensions;
using test_bp.Infrastructure.Security.Interfaces;
using test_bp.Shared.Messaging;

namespace test_bp.Features.User.Service
{
    public class UserService (
        IUserRepository repository,
        IPasswordHasher passwordHasher)
    {
        public async Task<ApiResponse<UserResponse>> CreateAsync(CreateUserRequest request, ClaimsPrincipal claimsPrincipal)
        {
            int companyId = claimsPrincipal.GetAgencyId();
            int userId = claimsPrincipal.GetUserId();

            /*if (companyId == 0 || userId == 0)
                return ApiResponse<UserResponse>.Unauthorized("Token inválido.");*/

            if (await repository.ExistsByUsernameAsync(request.UserName))
                return ApiResponse<UserResponse>.BadRequest("El nombre de usuario ya está registrado.");

            var passwordHash = passwordHasher.Hash(request.Password);

            var user = new Shared.Domain.Users(
                1, // para efectos de prueba
                request.RoleId,
                request.UserName,
                passwordHash,
                true
            );

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            return ApiResponse<UserResponse>.Created(UserHelper.ToResponse(
                user), "Usuario registrado correctamente");
        }

        public async Task<ApiResponse<UserResponse>> GetByIdAsync(int id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null) return ApiResponse<UserResponse>.NotFound("Usuario no encontrado.");

            return ApiResponse<UserResponse>.Ok(UserHelper.ToResponse(user));
        }
        public async Task<ApiResponse<List<UserResponse>>> GetAllUser()
        {
            var users = await repository.GetAll();
            return ApiResponse<List<UserResponse>>.Ok(users, "Lista de usuarios");
        }

        public async Task<ApiResponse<UserResponse>> UpdateUser(int idUser, UpdateUserRequest updateUserRequest)
        {
            var user = await repository.GetByIdAsync(idUser);
            if (user == null) return ApiResponse<UserResponse>.NotFound("Usuario no encontrado.");

            user.UserName = updateUserRequest.UserName;
            user.setStatus(updateUserRequest.IsActive); 

            repository.UpdateUser(user);
            await repository.SaveChangesAsync();

            return ApiResponse<UserResponse>.Ok(UserHelper.ToResponse(user), "Usuario actualizado correctamente");


        }
    }
}
