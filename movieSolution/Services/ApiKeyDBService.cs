using Microsoft.Extensions.Options;
using MongoDB.Driver;
using movieSolution.Models;

namespace movieSolution.Services;

public class ApiKeyDBService
{
    private readonly IMongoCollection<ApiKey> _keyCollection;

    public ApiKeyDBService(IOptions<ApiKeyDBSettings> keyDBSettings)
    {
        MongoClient client = new MongoClient(keyDBSettings.Value.ConnectionURI);

        IMongoDatabase database = client.GetDatabase(keyDBSettings.Value.DatabaseName);
        _keyCollection = database.GetCollection<ApiKey>(keyDBSettings.Value.CollectionName);
    }

    public async Task<ApiKey> GetAsync()
    {
        var filter = Builders<ApiKey>.Filter.Empty;
        return _keyCollection.Find(filter).ToList().First();
    }
    
}