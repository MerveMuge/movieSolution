using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using movieSolution.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace movieSolution.Services;

public class ApiKeyDBService
{
    private readonly IMongoCollection<ApiKey> _keyCollection;

    public ApiKeyDBService(IOptions<KeyDBSettings> keyDBSettings)
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