namespace MiniCatalog.Domain.Common;

public abstract class BaseLogModel
{
    public Guid LogId { get; protected set; } = Guid.NewGuid();

    public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;
    
}