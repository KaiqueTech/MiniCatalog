using MiniCatalog.Application.DTOs.Categoria;

namespace MiniCatalog.Application.DTOs.Item;

public class ItemSearchDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public decimal Preco { get; set; }
    public bool Ativo { get; set; }

    public CategoriaResponseDto Categoria { get; set; } = default!;
    public List<string> Tags { get; set; } = new();
}