using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EMIM.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EMIM.Services;
using EMIM.Data;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmimContext _emimContext;

    public HomeController(ILogger<HomeController> logger, EmimContext emimcontext)
    {
        _logger = logger;
        _emimContext = emimcontext;
    }

    public IActionResult Index()
    {
             return View(); 
    }

    public IActionResult Products(int? categoryId, string query)
    {
        var selectedCategory = _emimContext.Categories.FirstOrDefault(c => c.Id == categoryId);
        ViewData["SelectedCategoryId"] = categoryId;
        ViewData["SelectedCategoryName"] = selectedCategory?.Name;
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
