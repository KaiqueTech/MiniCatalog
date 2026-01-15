using FluentValidation;
using MiniCatalog.Application.DTOs.Auth;

namespace MiniCatalog.Application.Validators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O formato do e-mail é inválido.");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}