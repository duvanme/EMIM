using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EMIM.Controllers
{
    public class userController : Controller
    {
        private readonly IAccountService accountService;

        public userController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult UserProfile() => View();

        [HttpGet]
        public async Task<IActionResult> UserProfile(string Id)
        {
            var user = await accountService.FindUserByIdAsync(Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Something is wrong!");
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public IActionResult EditProfile() => View();
            public IActionResult UsuariosBloqueados() => View();
        
    }
}
