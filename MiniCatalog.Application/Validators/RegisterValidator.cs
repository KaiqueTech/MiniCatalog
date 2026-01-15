using FluentValidation;
using MiniCatalog.Application.DTOs.Auth;

namespace MiniCatalog.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
            .MinimumLength(3).WithMessage("O nome de usuário deve ter no mínimo 3 caracteres.");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O formato do e-mail é inválido.");
        
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
            .Matches(@"[0-9]").WithMessage("A senha deve conter ao menos um número.");
    }
}