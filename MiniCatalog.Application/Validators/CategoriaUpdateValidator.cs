using FluentValidation;
using FluentValidation.Validators;
using MiniCatalog.Application.DTOs.Categoria;

namespace MiniCatalog.Application.Validators;

public class CategoriaUpdateValidator : AbstractValidator<CategoriaUpdateDto>
{
    public CategoriaUpdateValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MaximumLength(50).WithMessage("O nome deve ter no máximo 50 caracteres.");

        RuleFor(c => c.Descricao)
            .MaximumLength(200).WithMessage("A descrição não pode exceder 200 caracteres.");
    }
}