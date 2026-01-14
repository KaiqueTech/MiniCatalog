
using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class AuditLogModel : BaseEntityLog
{
    public string Action { get; private set; }
    public object Payload { get; private set; }
    public Guid? UserId{ get; private set; }

    public AuditLogModel(string action, object payload, Guid? userId)
    {
        UserId = userId;
        Action = action;
        Payload = payload;
    }
}