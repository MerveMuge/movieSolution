using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace movieSolution.Models;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("search_token")]
    public string search_token { get; set; } = null!;

    [BsonElement("imdbID")]
    public string imdbID { get; set; } = null!;

    [BsonElement("processing_time_ms")]
    public decimal processing_time_ms { get; set; }

    [BsonElement("timestamp")]
    public DateTime timestamp { get; set; }

    [BsonElement("ip_address")]
    public string ip_address { get; set; } = null!;
}
