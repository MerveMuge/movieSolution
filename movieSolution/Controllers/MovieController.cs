
using MaxMind.GeoIP2.Exceptions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using movieSolution.Models;
using movieSolution.Services;

namespace movieSolution.Controllers;

[Route("/[controller]/{apiKey}")]
[ApiController]
public class MovieController : Controller
{
    private readonly MongoDBService _mongoDBService;
    private readonly ApiKeyDBService _keyDBSerice;
    private readonly ILogger<MovieController> _logger;

    public MovieController(MongoDBService mongoDBService, ILogger<MovieController> logger, ApiKeyDBService keyDBSerice)
    {
        _mongoDBService = mongoDBService;
        _logger = logger;
        _keyDBSerice = keyDBSerice;
    }

    [HttpGet()]
    public async Task<IActionResult> Get(string apiKey)
    {
        var admin = await _keyDBSerice.GetAsync();
        if(apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var res = await _mongoDBService.GetAsync();
        return Ok(res);
    }

    [HttpGet("search/title/{title}")]
    public async Task<IActionResult> GetMovieByTitle(string title, string apiKey)
    {
        var admin = await _keyDBSerice.GetAsync();
        if (apiKey != admin.key)
        {
             _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var response = await _mongoDBService.GetAsyncByTitle(title);
        return Ok(response);
    }

    
    [HttpGet("search/ImbdId/{imdbId}")]
    public async Task<IActionResult> GetMovieByImdbId(string imdbId, string apiKey)
    {
        var admin = await _keyDBSerice.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var response = await _mongoDBService.GetAsyncByImdbId(imdbId);
        return Ok(response);
    }

    [HttpGet("search/period")]
    public async Task<IActionResult> GetMovieByDates(string apiKey, int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
    {
        var admin = await _keyDBSerice.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var dateStart = new DateTime(startYear, startMonth, startDay);
        var dateEnd = new DateTime(endYear, endMonth, endDay);

        var response = await _mongoDBService.GetAsyncByDates(dateStart, dateEnd);
        return Ok(response);
    }

    [HttpGet("counter")]
    public async Task<IActionResult> Counter(string apiKey)
    {
        var admin = await _keyDBSerice.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var response = await _mongoDBService.Counter();
        return Ok(response);
    }  

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, string apiKey)
    {
        var admin = await _keyDBSerice.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }

}
