using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context;

namespace MiniCatalog.Infra.Persistence.Repositories;

public class AuditLogLogRepository : IAuditLogRepository
{
    private AuditDbContext _auditDbContext;

    public AuditLogLogRepository(AuditDbContext auditDbContext)
    {
        _auditDbContext = auditDbContext;
    }
    public async Task AddAsync(AuditLogModel log)
    {
        await _auditDbContext.AuditLogs.InsertOneAsync(log);
    }
}