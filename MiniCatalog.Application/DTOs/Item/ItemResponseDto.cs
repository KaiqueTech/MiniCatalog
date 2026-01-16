namespace MiniCatalog.Application.DTOs.Item;

public record ItemResponseDto(
    Guid Id,
    string Nome,
    string? Descricao,
    decimal Preco,
    string Categoria,
    List<string> Tags,
    bool Ativo,
    DateTime? DataCadastro
);