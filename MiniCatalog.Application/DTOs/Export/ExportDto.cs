namespace MiniCatalog.Application.DTOs.Export;

public record ExportDto(string Title,
    decimal Price,
    string Description,
    string Category);