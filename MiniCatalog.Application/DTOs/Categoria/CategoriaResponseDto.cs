namespace MiniCatalog.Application.DTOs.Categoria;

public class CategoriaResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; } = null;
    public bool Ativa { get; set; }
}