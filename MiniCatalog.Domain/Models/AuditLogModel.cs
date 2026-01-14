using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class AuditLogModel : BaseEntity
{
    public Guid LogId { get; private set; }
    public string Action { get; private set; }
    public Guid? UserId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public object Payload { get; private set; }

    public AuditLogModel(string action, Guid? userId, object payload)
    {
        Id = Guid.NewGuid();
        Action = action;
        UserId = userId;
        Payload = payload;
        Timestamp = DateTime.UtcNow;
    }
}