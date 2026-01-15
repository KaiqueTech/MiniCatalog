namespace MiniCatalog.Application.DTOs.Import;

public record ImportResultDto(
    int TotalFetched, 
    int Imported, 
    int Skipped, 
    List<string> Messages);