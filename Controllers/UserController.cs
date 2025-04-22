using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EMIM.Models;
using EMIM.ViewModels;
using EMIM.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using EMIM.ViewModel;

namespace EMIM.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        private readonly IFavoriteService _favoriteService;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAccountService accountService,
            IUserService userService,
            IFileService fileService,
            IWebHostEnvironment environment,
            IFavoriteService favoriteService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _userService = userService;
            _fileService = fileService;
            _environment = environment;
            _favoriteService = favoriteService;

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
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList(),
                CreatedAt = user.CreatedAt,
                UserProfilePicture = user.UserProfilePicture,

            };
            userViewModel.FavoriteProducts = await _favoriteService.GetFavoriteProductsAsync(id);
            userViewModel.FavoriteCount = await _favoriteService.GetFavoriteCountAsync(id);

            return View(userViewModel);
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {

            var userId = _userManager.GetUserId(User); // Get the logged-in user ID
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }


            var model = new EditUserProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                UserProfilePicturePath = user.UserProfilePicture
            };

            return View(model);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProfile", model); // Ensure it returns the correct view on validation errors
            }

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            if (model.UserProfilePicture != null && model.UserProfilePicture.Length > 0)
            {
                // Delete old picture if exists
                if (!string.IsNullOrEmpty(user.UserProfilePicture))
                {
                    _fileService.DeleteFile(user.UserProfilePicture);
                }

                // Save new picture
                var folderPath = "uploads/";
                var filePath = await _fileService.SaveFileAsync(model.UserProfilePicture, folderPath);

                if (filePath != null)
                {
                    user.UserProfilePicture = filePath;
                }
            }

            // Update user properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.Email = model.Email;
            user.UserName = model.Email; // Update username to match email
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = (DateTime)model.DateOfBirth;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("UserProfile", new { id = user.Id });
                // Redirect to UserProfile after successful update
            }

            ModelState.AddModelError("", "Failed to update profile.");
            return View("EditProfile", model); // Explicitly reference the correct view on failure
        }

        [Authorize]
        public IActionResult UsuariosBloqueados() => View();

        [Authorize]
public async Task<IActionResult> Favorites()
{
    var userId = _userManager.GetUserId(User);
    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var favoriteProducts = await _favoriteService.GetFavoriteProductsAsync(userId);
    var favoriteCount = await _favoriteService.GetFavoriteCountAsync(userId);

    var viewModel = new FavoritesViewModel
    {
        Products = favoriteProducts,
        Count = favoriteCount
    };

    return View(viewModel);
}
    }
}
