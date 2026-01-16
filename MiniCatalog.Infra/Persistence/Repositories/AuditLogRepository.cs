using System.Text.Json;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context;

namespace MiniCatalog.Infra.Persistence.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private AuditDbContext _auditDbContext;
    private readonly string _filePath;

    public AuditLogRepository(AuditDbContext auditDbContext)
    {
        _auditDbContext = auditDbContext;
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            "audit_logs.json");
    }
    public async Task AddAsync(AuditLogModelModel log)
    {
        try
        {
            await _auditDbContext.AuditLogs.InsertOneAsync(log);
        }
        catch (Exception)
        {
            await SaveToFileAsync(log);
        }
        
    }

    public async Task SaveToFileAsync(AuditLogModelModel log)
    {
        var json = JsonSerializer.Serialize(log);
        await File.AppendAllTextAsync(_filePath, json);
    }
}