using Microsoft.AspNetCore.Mvc;
using movieSolution.Services;

namespace movieSolution.Controllers;

[Route("/[controller]/{apiKey}")]
[ApiController]
public class MovieController : Controller
{
    private readonly MovieDBService _movieDBService;
    private readonly ApiKeyDBService _apiKeyDBService;
    private readonly ILogger<MovieController> _logger;

    public MovieController(MovieDBService movieDBService, ILogger<MovieController> logger, ApiKeyDBService apiKeyDBService)
    {
        _movieDBService = movieDBService;
        _logger = logger;
        _apiKeyDBService = apiKeyDBService;
    }

    [HttpGet()]
    public async Task<IActionResult> Get(string apiKey)
    {
        var admin = await _apiKeyDBService.GetAsync();
        if(apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var res = await _movieDBService.GetAsync();
        return Ok(res);
    }

    [HttpGet("search/title/{title}")]
    public async Task<IActionResult> GetMovieByTitle(string title, string apiKey)
    {
        var admin = await _apiKeyDBService.GetAsync();
        if (apiKey != admin.key)
        {
             _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var response = await _movieDBService.GetAsyncByTitle(title);
        return Ok(response);
    }

    
    [HttpGet("search/ImbdId/{imdbId}")]
    public async Task<IActionResult> GetMovieByImdbId(string imdbId, string apiKey)
    {
        var admin = await _apiKeyDBService.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var response = await _movieDBService.GetAsyncByImdbId(imdbId);
        return Ok(response);
    }

    [HttpGet("search/period")]
    public async Task<IActionResult> GetMovieByDates(string apiKey, int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
    {
        var admin = await _apiKeyDBService.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var dateStart = new DateTime(startYear, startMonth, startDay);
        var dateEnd = new DateTime(endYear, endMonth, endDay);

        var response = await _movieDBService.GetAsyncByDates(dateStart, dateEnd);
        return Ok(response);
    }

    [HttpGet("counter")]
    public async Task<IActionResult> Counter(string apiKey)
    {
        var admin = await _apiKeyDBService.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        var response = await _movieDBService.Counter();
        return Ok(response);
    }  

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, string apiKey)
    {
        var admin = await _apiKeyDBService.GetAsync();
        if (apiKey != admin.key)
        {
            _logger.LogError("Authorization failed.");
            return Unauthorized();
        }
        await _movieDBService.DeleteAsync(id);
        return NoContent();
    }

}
