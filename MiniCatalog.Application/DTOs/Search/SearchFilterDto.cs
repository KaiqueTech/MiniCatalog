namespace MiniCatalog.Application.DTOs.Search;

public record SearchFilterDto(
    string? Term = null,
    Guid? CategoriaId = null,
    decimal? Min = null,
    decimal? Max = null,
    bool? Ativo = null,
    string? Tags = null,
    string? Sort = "nome",
    int Page = 1,
    int PageSize = 10
);