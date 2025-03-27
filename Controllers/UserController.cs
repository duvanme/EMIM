using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EMIM.Models;
using EMIM.ViewModels;
using EMIM.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
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
            // Si no se proporciona un ID, usa el del usuario autenticado
            if (string.IsNullOrEmpty(id))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Index", "Home"); // Si no está autenticado, redirige
                }
                id = currentUser.Id;
            }

            var user = await _accountService.FindUserByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuario no encontrado.");
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

        [Authorize]
        public IActionResult EditProfile() => View();

        [Authorize]
        public IActionResult UsuariosBloqueados() => View();
    }
}
