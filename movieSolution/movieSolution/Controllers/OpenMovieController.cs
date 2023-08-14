using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using movieSolution.Models;
using movieSolution.Services;

namespace movieSolution.Controllers;

[Route("/[controller]")]
[ApiController]
public class OpenMovieController : Controller
{
    private static HttpClient? _httpClient;
    public OpenMovieController()
    {
        _httpClient = new HttpClient();
    }
    [HttpGet("/search")]
    public async Task<IActionResult> GetMovieByTitle(string movieTitle)
    {
        var omdbapiResponse = await _httpClient.GetStringAsync("http://www.omdbapi.com/?t=" + movieTitle + "&plot=full&apikey=4b06faa2");
        var response = JsonConvert.DeserializeObject<OpenMovie>(omdbapiResponse);
        return Ok(response);
    }

}
