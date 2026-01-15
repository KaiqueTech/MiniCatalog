using FluentValidation;
using MiniCatalog.Application.DTOs.Item;

namespace MiniCatalog.Application.Validators;

public class ItemRequestValidator : AbstractValidator<ItemRequestDto>
{
    public ItemRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(x => x.CategoriaId)
            .NotEmpty().WithMessage("O Id da categoria é obrigatória.");

        RuleFor(x => x.Descricao)
            .MaximumLength(500).WithMessage("A descrição não pode exceder 500 caracteres.");
    }
}