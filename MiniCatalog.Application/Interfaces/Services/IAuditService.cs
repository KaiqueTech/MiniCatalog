using MiniCatalog.Application.DTOs.Audit;

namespace MiniCatalog.Application.Interfaces.Services;

public interface IAuditService
{
    Task AuditLogAsync(AuditLogDto dto);
}