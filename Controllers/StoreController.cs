using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class StoreController : Controller
    {
        
        public IActionResult StoreProfile() => View();
        public IActionResult CreateStore() => View();
        public IActionResult TiendasBloqueadas() => View();
    }
    
}
