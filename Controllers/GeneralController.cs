using EMIM.Models;
using EMIM.Views.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class GeneralController : Controller
    {

        public IActionResult Carge()
        {
            return RedirectToAction("Login", "Account");
        }
        public IActionResult ShoppingCar()
        {
            Console.WriteLine($"User Authenticated: {User.Identity?.IsAuthenticated}");
            Console.WriteLine($"User Name: {User.Identity?.Name}");

            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                TempData["Mensaje"] = "Debes iniciar sesión para acceder al carrito de compras.";
                return RedirectToAction("Carge", "General" );
            }
            return View();
        }

        public IActionResult Reviews() => View();
        public IActionResult Help() => View();
        public IActionResult UnderConstruction() => View();     
    }
}
