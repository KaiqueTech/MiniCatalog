namespace MiniCatalog.Application.DTOs.Item;

public class ItemRequestDto
{
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public decimal Preco { get; set; }
    public List<string> Tags { get; set; } = new();
}