using EMIM.Models;
using EMIM.ViewModels;
using EMIM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EMIM.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string id)
        {
            var user = await _accountService.FindUserByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "Something is wrong!");
                return RedirectToAction("Index", "Home");
            }

            var roles = await _userManager.GetRolesAsync(user);
            
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles.ToList(),
                CreatedAt = user.CreatedAt
            };

            return View(userViewModel);
        }

        public IActionResult EditProfile() => View();
        public IActionResult UsuariosBloqueados() => View();
    }
}
