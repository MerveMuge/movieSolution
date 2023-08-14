using Microsoft.AspNetCore.Mvc;
using movieSolution.Models;
using movieSolution.Services;

namespace movieSolution.Controllers;

[Route("/[controller]")]
[ApiController]
public class ApiKeyController : Controller
{
    private readonly ApiKeyDBService _apiKeyDBService;

    public ApiKeyController(ApiKeyDBService apiKeyDBService)
    {
        _apiKeyDBService = apiKeyDBService;

    }

    [HttpGet("key")]
    public async Task<ApiKey> Get()
    {
        return await _apiKeyDBService.GetAsync();
    }

}