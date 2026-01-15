using System.ComponentModel.DataAnnotations;

namespace MiniCatalog.Application.DTOs.Audit;

public record AuditLogDto
(
    Guid LogId ,
    string Action ,
    object Payload ,
    Guid UserId ,
    DateTime Timestamp 
);