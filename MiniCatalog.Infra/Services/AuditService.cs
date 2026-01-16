using MiniCatalog.Application.DTOs.Audit;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context;

namespace MiniCatalog.Infra.Services;

public class AuditService : IAuditService
{
    private readonly IAuditLogRepository _auditRepository;

    public AuditService(IAuditLogRepository auditRepository)
    {
        _auditRepository = auditRepository;
    }

    public async Task AuditLogAsync(AuditLogDto dto)
    {
        var log = new AuditLogModelModel(dto.Action, dto.Payload, dto.UserId);
        
        await _auditRepository.AddAsync(log);
    }
}