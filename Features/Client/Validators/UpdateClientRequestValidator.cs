using FluentValidation;
using test_bp.Features.Client.DTOs;

namespace test_bp.Features.Client.Validators
{
    public class UpdateClientRequestValidator : AbstractValidator<UpdateClientRequest>
    {
        public UpdateClientRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.")
                .MaximumLength(100).WithMessage("El apellido no puede superar los 100 caracteres.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
                .When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
