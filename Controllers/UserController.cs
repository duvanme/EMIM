using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using EMIM.Data;
using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net.Sockets;
using EMIM.ViewModel;
using System.Threading.Tasks;

namespace EMIM.Controllers
{
    [Authorize] // 🔒 Restringe el acceso solo a usuarios autenticados
    public class UserController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly CloudinaryService _cloudinaryService;

        public UserController(UserManager<User> userManager, CloudinaryService cloudinaryService)
        {
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
        }

        public IActionResult UserProfile() => View();
        public IActionResult EditProfile() => View();

        // Obtener información del usuario actualmente autenticado y mostrarla en el perfil//
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtener el ID del usuario autenticado

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); //Bloquear acceso si no está autenticado
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Address,
                user.Email,
                user.PhoneNumber,
                BirthDate = user.BirthDate?.ToString("yyyy-MM-dd"),
                ImageProfile = user.ProfileImageUrl
            });
        }

        //Actualizar perfil del usuario
        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileUpdateViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            user.Address = model.Address;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = DateTime.Parse(model.BirthDate);
            user.ProfileImageUrl = model.ImageProfile; //Guardar la imagen en la BD

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("Perfil actualizado correctamente.");
            }

            return BadRequest($"Error al actualizar el perfil: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        //Subir imagen de perfil y guardar su URL
        [HttpPut("UploadProfileImage")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No se subió ninguna imagen.");


            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized("Usuario no autenticado.");

            var imageUrl = await _cloudinaryService.UploadImageAsync(file);
            if (imageUrl == null)
                return BadRequest("Error al subir la imagen.");


            user.ProfileImageUrl = imageUrl;
            await _userManager.UpdateAsync(user);

            return Ok(new { ImageUrl = imageUrl });
        }

        public IActionResult UsuariosBloqueados() => View();

    }
}
