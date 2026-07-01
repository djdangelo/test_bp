using FluentValidation;
using test_bp.Features.Auth.DTOs;
using test_bp.Features.User.Interface;
using test_bp.Infrastructure.Security.Interfaces;
using test_bp.Shared.Messaging;

namespace test_bp.Features.Auth.Service
{
    public class AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IValidator<LoginRequest> loginValidator,
        ILogger<AuthService> logger)
    {
        public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
        {
            logger.LogInformation("Intento de inicio de sesión recibido para el nombre de usuario: {UserName}", request.UserName);

            var validation = await loginValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                logger.LogWarning("Fallo en la validación de propiedades del inicio de sesión para {UserName}: {Error}", 
                    request.UserName, validation.Errors.FirstOrDefault()?.ErrorMessage);
                return ApiResponse<AuthResponse>.BadRequest(validation.Errors.FirstOrDefault()!.ErrorMessage);
            }

            var user = await userRepository.GetByUserNameAsync(request.UserName);

            if (user == null || !user.IsActive || !passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                logger.LogWarning("Inicio de sesión fallido: credenciales incorrectas o usuario inactivo para {UserName}", request.UserName);
                return ApiResponse<AuthResponse>.Unauthorized("Credenciales incorrectas o usuario inactivo.");
            }

            var token = jwtProvider.GenerateUserToken(user.Id, user.AgencyId, user.Roles.Name);

            var profile = new UserProfile(user.Id, user.AgencyId, user.UserName, user.Roles.Name);

            logger.LogInformation("Inicio de sesión exitoso para {UserName}. ID de Usuario: {UserId}, Roles: {Roles}", 
                request.UserName, user.Id, string.Join(", ", user.Roles.Name));

            return ApiResponse<AuthResponse>.Ok(new AuthResponse(token, profile), "Login exitoso.");
        }
    }
}
