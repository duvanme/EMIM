using EMIM.Models;
using EMIM.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace EMIM.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountService(
        SignInManager<User> signInManager, 
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IEmailService emailService, 
        IHttpContextAccessor httpContextAccessor)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.httpContextAccessor = httpContextAccessor;
            this.emailService = emailService;

        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Address = model.Address,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                EmailConfirmed = false,
                DateOfBirth = model.DateOfBirth
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleName = model.Role.ToString();
                if (string.IsNullOrEmpty(roleName))
                {
                    roleName = "Customer";
                }

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                await userManager.AddToRoleAsync(user, roleName);

                // Generar token de confirmación de correo (opcional en local)
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"http://localhost:5136/Account/EmailConfirm?userId={user.Id}&token={token}";

                var message = $"<h1>Bienvenido a EMIM</h1><p>Tu cuenta ha sido creada correctamente.</p>";

                var emailSent = await emailService.SendEmailAsync(user.Email, "Confirma tu correo en EMIM", message);

                if (emailSent)
                {
                    user.EmailConfirmed = true; // Solo marcar como confirmado si el correo se envió
                    await userManager.UpdateAsync(user);
                }
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            // Obtener el contexto HTTP actual
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                // Añadir una cookie para indicar la limpieza del carrito
                httpContext.Response.Cookies.Append("ClearCart", "true", new CookieOptions
                {
                    HttpOnly = false, // Debe ser false para que JavaScript pueda leerlo
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(1) // Expira en 1 minuto
                });
            }

            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            var removeResult = await userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return removeResult;

            return await userManager.AddPasswordAsync(user, model.NewPassword);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }
    }
}
