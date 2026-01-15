using FluentValidation;
using MiniCatalog.Application.DTOs.Auth;

namespace MiniCatalog.Application.Validators;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(u => u.NovoEmail)
            .EmailAddress()
            .WithMessage("E-mail inválido.");
        
        RuleFor(u => u.NovaSenha)
            .MinimumLength(6).
            WithMessage("A nova senha deve ter no mínimo 6 caracteres.")
            .Matches(@"[A-Z]").
            WithMessage("Deve conter uma letra maiúscula.");
    }
}