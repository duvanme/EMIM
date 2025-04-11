using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EMIM.Models;

namespace EMIM.Controllers;

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

    public IActionResult Products(int? categoryId)
    {
        ViewData["SelectedCategoryId"] = categoryId;
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
}
