using MiniCatalog.Application.DTOs.Import;

namespace MiniCatalog.Application.Interfaces.Services;

public interface IImportService
{
    Task<ImportResultDto> ImportFromExternalApiAsync(Guid userId);
}