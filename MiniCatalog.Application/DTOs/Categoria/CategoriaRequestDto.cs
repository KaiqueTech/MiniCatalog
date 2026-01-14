namespace MiniCatalog.Application.DTOs.Categoria;

public class CategoriaRequestDto
{
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
}