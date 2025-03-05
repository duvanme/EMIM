using Microsoft.AspNetCore.Mvc;
using EMIM.Services;
using EMIM.ViewModel;

public class AdminController : Controller
{

    //Servicio para la gestión de usuarios
    private readonly IUserService _userService;

    //Mostrar la vista del perfil administrador 
    public IActionResult AdminProfile() => View();


    //Constructor que inyecta el servicio de usuario
    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    //Mostrar la vista para crear un nuevo usuario
    public IActionResult RegisterUser() => View();

    //Registrar un nuevo usuario
    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserViewModel model)
    {
        //Verifica si el modelo es valido
        if (!ModelState.IsValid) return View(model);
        var result = await _userService.RegisterUserAsync(model);
        if (result.Succeeded)
            return RedirectToAction("Login", "Account");

        //Errores por si el modelo falla
        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);
    }

    public IActionResult VerifyEmail() => View();

    //Verficar el correo
    [HttpPost]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        //Busca al usuario por el correo
        var user = await _userService.FindUserByEmailAsync(model.Email);
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
    
        var result = await _userService.ChangePasswordAsync(model);
        // Si el cambio es exitoso, redirige a la página de inicio de sesión
        if (result.Succeeded)
            return RedirectToAction("Login");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);
    }



}
