namespace MiniCatalog.Application.DTOs.Categoria;

public record CategoriaResponseDto(Guid Id, string Nome, string? Descricao, bool Ativo);