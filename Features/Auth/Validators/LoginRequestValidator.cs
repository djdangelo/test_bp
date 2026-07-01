using FluentValidation;
using test_bp.Features.Auth.DTOs;

namespace test_bp.Features.Auth.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.");
        }
    }
}
