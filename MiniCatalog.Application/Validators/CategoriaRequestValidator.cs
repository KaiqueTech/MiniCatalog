using FluentValidation;
using MiniCatalog.Application.DTOs.Categoria;

namespace MiniCatalog.Application.Validators;

public class CategoriaRequestValidator : AbstractValidator<CategoriaRequestDto>
{
    public CategoriaRequestValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .Length(3, 100).WithMessage("O nome da categoria deve ter entre 3 e 100 caracteres.");
        
        RuleFor(c => c.Descricao)
            .MaximumLength(500)
            .WithMessage("A descrição não pode exceder 500 caracteres.");
    }
}