using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Interfaces.Repositories;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLogModel log);
    Task SaveToFileAsync(AuditLogModel log);
}