using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EMIM.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public IActionResult Products(int? categoryId, string query)
    {
        ViewData["SelectedCategoryId"] = categoryId;
        ViewData["Query"] = query;
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
