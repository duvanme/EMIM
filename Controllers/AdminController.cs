using Microsoft.AspNetCore.Mvc;
using EMIM.Services;
using EMIM.ViewModel;
using EMIM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EMIM.Models;
using Microsoft.AspNetCore.Identity;
using Stripe.V2;

namespace EMIM.Controllers
{
    public class AdminController : Controller
{

    private readonly EmimContext _context;
    //Servicio para la gestión de usuarios
    private readonly IAdminService _adminService;
    private readonly IStoreService _storeService;
    private readonly IEmailService _emailService;
    private readonly UserManager<User> _userManager;

        //Constructor que inyecta el servicio de usuario

        public AdminController(IAdminService adminService, IStoreService storeService, EmimContext context, IEmailService emailService, UserManager<User> userManager)
    {
        _adminService = adminService;
        _storeService = storeService;
        _context = context;
        _emailService = emailService;
        _userManager = userManager;
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
                    Nit = t.Nit,
                    Location = t.Location,
                    //StoreProfilePicture = t.StoreProfilePicture
                }).ToList()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdminCreationShop(int storeId, string userId)
        {    

            var store = _context.Stores.FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            await _storeService.AcceptCreateStore(store);
            await _storeService.AssignVendorRoleAsync(user);

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

        //Mostrar la vista del perfil administrador 
        [Authorize(Roles = "Admin")]
        public IActionResult AdminProfile() => View();

        public IActionResult HelpQuestion()
        {
            var preguntas = _context.HelpQuestions.ToList();
            return View(preguntas);
        }

        [HttpPost]
        [Route("HelpQuestion/Submit")]
        public async Task<IActionResult> Submit([FromBody] HelpQuestion model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HelpQuestions.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AnswerHelpQuestion(int Id, string Answer)
        {
            var question = await _context.HelpQuestions.FindAsync(Id);
            if (question == null)
                return NotFound();

            question.Answer = Answer;
            question.Status = "Respondido";

            await _context.SaveChangesAsync();

            // Enviar correo al usuario
            await _emailService.SendEmailAsync(
                question.Email,
                $"Respuesta a tu solicitud: {question.Subject}",
                $"Hola {question.UserName},\n\nGracias por contactarnos. Esta es la respuesta a tu mensaje:\n\n\"{question.Message}\"\n\nRespuesta del administrador:\n\"{Answer}\""
            );

            return RedirectToAction("HelpQuestion");
        }


    }

}