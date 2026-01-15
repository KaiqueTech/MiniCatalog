namespace MiniCatalog.Application.DTOs.Import;

public record ImportDto(
    string Title, 
    decimal Price, 
    string Description, 
    string Category);