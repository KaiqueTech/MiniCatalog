using MiniCatalog.Domain.Models;
using MongoDB.Driver;

namespace MiniCatalog.Infra.Persistence.Context;

public class AuditDbContext
{
    private readonly IMongoDatabase _database;

    public IMongoCollection<AuditLogModel> AuditLogs { get; }

    public AuditDbContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
        
        AuditLogs = _database.GetCollection<AuditLogModel>("AuditLogs");
    }
}