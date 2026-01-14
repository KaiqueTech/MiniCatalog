using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace MiniCatalog.Api.Configurations;

public static class MongoGuidConfiguration
{
    public static void Configure()
    {
        BsonSerializer.RegisterSerializer(
            new GuidSerializer(GuidRepresentation.Standard)
        );
    }
}