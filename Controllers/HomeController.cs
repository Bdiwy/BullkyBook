using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BullkyBook.Models;
using BullkyBook.Services;

namespace BullkyBook.Controllers;

public class HomeController : Controller
{
    private readonly WeatherService _weatherService ;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger , WeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult DashBoard()
    {
        return View();
    }

    [HttpGet]
    [Route("api/weather-now")]
    public async Task<JsonResult> GetWeather()
    {
        var data = await _weatherService.GetWeatherAsync();
        return new JsonResult(data);
    }

    [Route("notfound", Name = "notfound")]  
    public IActionResult NotFound()
    {
        Response.StatusCode = 404; 
        return View();             
    }
}
