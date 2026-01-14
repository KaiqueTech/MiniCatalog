using MiniCatalog.Application.DTOs.Audit;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context;

namespace MiniCatalog.Infra.Services;

public class AuditService : IAuditService
{
    private readonly IAuditLogRepository _auditRepository;
    private readonly string _logFilePath;

    public AuditService(IAuditLogRepository auditRepository)
    {
        _auditRepository = auditRepository;
        _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
    }

    public async Task AuditLogAsync(AuditLogDto dto)
    {
        var log = new AuditLogModel(dto.Action, dto.UserId, dto.Payload);
        
        await _auditRepository.AddAsync(log);
    }
}