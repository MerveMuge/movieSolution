using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using ThirdParty.BouncyCastle.Asn1;
using movieSolution.Models;

namespace movieSolution.Services;

public class MongoDBService
{

    private readonly IMongoCollection<Movie> _movieCollection;


    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _movieCollection = database.GetCollection<Movie>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Movie>> GetAsync()
    {
        return await _movieCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Movie> GetAsyncByTitle(string title)
    {
        Movie movie = await _movieCollection.Find(c => c.search_token == title).FirstOrDefaultAsync();
        return movie;
    }

    public async Task<Movie> GetAsyncByImdbId(string imdbId)
    {
        Movie movie = await _movieCollection.Find(c => c.imdbID == imdbId).FirstOrDefaultAsync();
        return movie;
    }

    public async Task<List<Movie>> GetAsyncByDates(DateTime start, DateTime end)
    {
        return await _movieCollection.Find(c => (c.timestamp > start && c.timestamp < end) ).ToListAsync();
        
    }
    public async Task<object> Counter()
    {
        var embeddedDocFieldAggregate = _movieCollection
            .Aggregate()
            .Group(u => u.timestamp.Date, // embedded document
            ac => new
            {
                date = ac.Select(x => x.timestamp).First().ToString("%Y-%m-%d"),
                count = ac.Count() });

        var groupedPerCountry = await embeddedDocFieldAggregate.ToListAsync();
        return groupedPerCountry;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Movie> filter = Builders<Movie>.Filter.Eq("Id", id);
        await _movieCollection.DeleteOneAsync(filter);
        return;
    }

}

