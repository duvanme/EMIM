using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductDisplay() => View();
        public IActionResult NewProduct() => View();
        public IActionResult EditProduct() => View();
        public IActionResult ProductosBloqueados() => View();
    }
}
