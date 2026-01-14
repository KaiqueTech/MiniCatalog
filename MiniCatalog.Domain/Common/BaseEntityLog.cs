namespace MiniCatalog.Domain.Common;

public abstract class BaseEntityLog
{
    public Guid LogId { get; protected set; } = Guid.NewGuid();

    public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;
    
}