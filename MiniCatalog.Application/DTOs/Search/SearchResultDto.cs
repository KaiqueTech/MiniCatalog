using MiniCatalog.Application.DTOs.Item;

namespace MiniCatalog.Application.DTOs.Search;

public record SearchResultDto(
    IEnumerable<ItemResponseDto> Items,
    int TotalItems,
    decimal AveragePrice,
    int Page,
    int TotalPages
);