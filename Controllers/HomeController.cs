using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BullkyBook.Models;

namespace BullkyBook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

    [Route("notfound", Name = "notfound")]  
    public IActionResult NotFound()
    {
        Response.StatusCode = 404; 
        return View();             
    }

    [Route("boom")]
    public IActionResult Boom()
    {
        throw new InvalidOperationException("Test 500 – something went wrong!");
    }
}
