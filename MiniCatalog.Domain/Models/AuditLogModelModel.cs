
using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class AuditLogModelModel : BaseLogModel
{
    public string Action { get; private set; }
    public object Payload { get; private set; }
    public Guid? UserId{ get; private set; }

    public AuditLogModelModel(string action, object payload, Guid? userId)
    {
        UserId = userId;
        Action = action;
        Payload = payload;
    }
}