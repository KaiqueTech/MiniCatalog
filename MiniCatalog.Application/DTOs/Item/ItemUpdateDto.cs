namespace MiniCatalog.Application.DTOs.Item;

public record ItemUpdateDto(
    string Nome, 
    string Descricao, 
    decimal Preco, 
    Guid CategoriaId, 
    List<string> Tags
);