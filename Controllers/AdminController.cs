using EMIM.Data;
using EMIM.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class AdminController : Controller
    {

        private readonly EmimContext context;

        public AdminController(EmimContext context)
        {
            this.context = context;
        }

        public IActionResult AdminNotification()
        {
            var stores = context.Stores
            .Where(s => s.StoreStatus == 1)
            .ToList();
            return View(stores);
        }

        public IActionResult AdminProfile() => View();
    }
}
