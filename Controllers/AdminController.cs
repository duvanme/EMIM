using Microsoft.AspNetCore.Mvc;
using EMIM.Services;
using EMIM.ViewModel;
using EMIM.Data;
using Microsoft.EntityFrameworkCore;

namespace EMIM.Controllers
{
    public class AdminController : Controller
{

    private readonly EmimContext _context;
    //Servicio para la gestión de usuarios
    private readonly IAdminService _adminService;
    private readonly IStoreService _storeService;

    //Constructor que inyecta el servicio de usuario

    public AdminController(IAdminService adminService, IStoreService storeService, EmimContext context)
    {
        _adminService = adminService;
        _storeService = storeService;
        _context = context;
    }

    //Mostrar la vista para crear un nuevo usuario
    public IActionResult RegisterUser() => View();

    //Registrar un nuevo usuario
    [HttpPost]
    public async Task<IActionResult> RegisterUser(AdminViewModel model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error de validación: {error.ErrorMessage}");
            }
            return View(model);
        }

        var result = await _adminService.RegisterUserAsync(model);
        if (result.Succeeded)
        {
            return RedirectToAction("AdminProfile", "Admin");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    public IActionResult VerifyEmail() => View();

    //Verficar el correo
    [HttpPost]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        //Busca al usuario por el correo
        var user = await _adminService.FindUserByEmailAsync(model.Email);
        if (user == null)
        {
            //Error si el usuario no existe
            ModelState.AddModelError("", "Something is wrong!");
            return View(model);
        }

        return RedirectToAction("ChangePassword", new { username = user.UserName });
    }


    //Mostrar la vista para cambiar la contraseña
    [HttpGet]
    public IActionResult ChangePassword()
    {
        //Iniciar el modelo con valores vacios
        var model = new ChangePasswordViewModel
        {
            Email = String.Empty,
            NewPassword = string.Empty,
            ConfirmNewPassword = string.Empty
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        //Verifica si el modelo es valido
        if (!ModelState.IsValid) return View(model);

        var result = await _adminService.ChangePasswordAsync(model);
        // Si el cambio es exitoso, redirige a la página de inicio de sesión
        if (result.Succeeded)
            return RedirectToAction("Login");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);
    }

        public IActionResult AdminCreationShop()
        {
         var users = _context.Users
        .Include(u => u.Stores) // Cargar las tiendas relacionadas
        .ToList();

            var viewModel = users.Select(u => new UserStoresViewModel
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Stores = u.Stores
                .Where(t => t.StoreStatus == "pending")
                .Select(t => new CreateStoreViewModel
                {
                    Name = t.Name,
                    StoreId = t.Id,
                    Description = t.Description,
                    //StoreProfilePicture = t.StoreProfilePicture
                }).ToList()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdminCreationShop(int storeId)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                return NotFound();
            }

            await _storeService.AcceptCreateStore(store);

            return RedirectToAction("AdminCreationShop");
        }

        [HttpPost]
        public async Task<IActionResult> AdminNegationShop(int storeId)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                return NotFound();
            }

            await _storeService.DenyCreateStore(store);

            return RedirectToAction("AdminCreationShop");
        }
    }
}

    
        //Mostrar la vista del perfil administrador 
        public IActionResult AdminProfile() => View();

}

