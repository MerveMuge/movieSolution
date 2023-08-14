using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace movieSolution.Models;

public class ApiKey
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("key")]
    public string key { get; set; } = null!;
}