using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class GeneralController : Controller
    {
        public IActionResult ShoppingCar() => View();
        public IActionResult Reviews() => View();
        public IActionResult Help() => View();
    }
}
