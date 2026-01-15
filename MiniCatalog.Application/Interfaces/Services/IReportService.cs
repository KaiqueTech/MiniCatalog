namespace MiniCatalog.Application.Interfaces.Services;

public interface IReportService
{
    Task<(byte[] Content, string FileName)> ExportItemsToCsvAsync();
}