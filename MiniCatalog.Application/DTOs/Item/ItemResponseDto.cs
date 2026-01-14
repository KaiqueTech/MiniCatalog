namespace MiniCatalog.Application.DTOs.Item;

public class ItemResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Categoria { get; set; }
    public List<string> Tags { get; set; }
    public decimal Preco { get; set; }
    public bool Ativo { get; set; }
}