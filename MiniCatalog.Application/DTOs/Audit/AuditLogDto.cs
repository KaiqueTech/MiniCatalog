using System.ComponentModel.DataAnnotations;

namespace MiniCatalog.Application.DTOs.Audit;

public class AuditLogDto
{
    public Guid LogId { get; set; }
    public required string Action { get; set; }
    public required object Payload { get; set; }
    public Guid UserId { get; set; }
    public DateTime Timestamp { get; set; }
}