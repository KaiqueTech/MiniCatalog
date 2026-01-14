namespace MiniCatalog.Application.DTOs.Audit;

public class AuditLogDto
{
    public Guid LogId { get; set; }
    public string Action { get; set; }
    public object Payload { get; set; }
    public Guid UserId { get; set; }
    public DateTime Timestamp { get; set; }
}