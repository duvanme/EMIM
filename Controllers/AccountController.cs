using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMIM.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService accountService;
        private readonly UserManager<User> userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<User> _signInManager;


        public AccountController(IAccountService accountService, UserManager<User> userManager, IEmailService _emailService, SignInManager<User> signInManager)
        {
            this.accountService = accountService;
            this.userManager = userManager;
            this._emailService = _emailService;
            _signInManager = signInManager;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await accountService.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(model.Email); // O usa model.Username si es con username

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (await userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("AdminProfile", "Admin"); // Vista del admin
                }
                else if (await userManager.IsInRoleAsync(user, "Vendor"))
                {
                    return RedirectToAction("StoreProfile", "Store"); // Vista del vendedor
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Vista por defecto o de cliente
                }
            }
            ModelState.AddModelError("", "Email or password is incorrect.");
            return View(model);
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (model.DateOfBirth > DateTime.Today.AddYears(-18))
            {
                ModelState.AddModelError("DateOfBirth", "Debes tener al menos 18 años.");
            }

            if (!ModelState.IsValid) return View(model);

            var result = await accountService.RegisterAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return BadRequest("Email confirmation failed.");
        }


        public IActionResult VerifyEmail() => View();

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await accountService.FindUserByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Something is wrong!");
                return View(model);
            }
            // Generar código aleatorio
            var code = new Random().Next(100000, 999999).ToString();

            // Enviar código por correo
            await _emailService.SendEmailAsync(model.Email, "Verification code.", $"Your code is: {code}");

            // Guardar el código en la sesión o base de datos para verificar después
            HttpContext.Session.SetString("VerificationCode", code);

            return RedirectToAction("VerifyCode", new { email = model.Email });
        }

        public IActionResult VerifyCode(string email)
        {
            return View(new VerifyCodeViewModel { Email = email });
        }

        [HttpPost]
        public IActionResult VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var storedCode = HttpContext.Session.GetString("VerificationCode");
            if (model.Code == storedCode)
            {
                return RedirectToAction("ChangePassword", new { username = model.Email });
            }

            ModelState.AddModelError("", "Wrong code.");
            return View(model);
        }



        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("VerifyEmail");

            return View(new ChangePasswordViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await accountService.ChangePasswordAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
