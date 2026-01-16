using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Interfaces.Repositories;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLogModelModel log);
    Task SaveToFileAsync(AuditLogModelModel log);
}