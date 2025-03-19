using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EMIM.Models;
using EMIM.ViewModel;
using EMIM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace EMIM.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        private readonly CloudinaryService _cloudinaryService;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IAccountService accountService, CloudinaryService cloudinaryService)
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

        public IActionResult UserProfile() => View();
        [Authorize]
        [HttpGet("EditProfile")]
        public async Task<IActionResult> EditProfileAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var model = new UserProfileUpdateViewModel
            {
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate?.ToString("yyyy-MM-dd"), // Asegúrate de que el formato sea correcto
                ImageProfile = user.ProfileImageUrl // URL de la imagen
            };

            return View(model);
        }

        //Obtener perfil del usuario
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new
            {
                Id = user.Id,
                user.Address,
                user.Email,
                user.PhoneNumber,
                BirthDate = user.BirthDate?.ToString("yyyy-MM-dd"),
                ImageProfile = user.ProfileImageUrl // URL de la imagen
            });
        }

        //Actualizar perfil del usuario
        [HttpPost("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileUpdateViewModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                return BadRequest(new { message = "The 'Id' field is required." });
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Id = model.Id;
            user.Address = model.Address;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = DateTime.Parse(model.BirthDate);
            user.ProfileImageUrl = model.ImageProfile; //Guardar la imagen en la BD

            //Asegurarse de actualizar la URL de la imagen
            /*if (!string.IsNullOrEmpty(model.ImageProfile))
            {
                user.ProfileImageUrl = model.ImageProfile;
            }*/

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                // Redirige a la vista de UserProfile
                //return RedirectToAction("UserProfile", "User");
                return Ok("Profile updated successfully.");
            }

            return BadRequest($"Error updating profile: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        //Subir imagen de perfil y guardar su URL
        [HttpPost("UploadProfileImage")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No image uploaded.");


            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized("User not authenticated.");

            var imageUrl = await _cloudinaryService.UploadImageAsync(file);
            if (imageUrl == null)
                return BadRequest("Image upload failed.");


            user.ProfileImageUrl = imageUrl;
            await _userManager.UpdateAsync(user);

            return Ok(new { ImageUrl = user.ProfileImageUrl }); //Devuelve la URL actualizada
        }
        public IActionResult UsuariosBloqueados() => View();

    }
}
